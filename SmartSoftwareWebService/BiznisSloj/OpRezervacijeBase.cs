using SmartSoftwareWebService.BiznisSloj;
using SmartSoftwareWebService.DataSloj;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SmartSoftwareWebService.BiznisSloj
{
    public class OpRezervacijeBase : OpOpremaBase
    {
        public DbItemOpremaSaParametrimaRezervacija DataSelectOpremaRezervacija { get; set; }

        protected List<DbItemOpremaSaParametrima> vratiOpremuZaRezervacije(int id_rezervacije, SmartSoftwareBazaEntities entities)
        {
            List<DbItemOpremaSaParametrima> Lista =
                (
                    from proizvodOpreme in entities.opremas
                    join rezOprema in entities.RezervacijeOpremas
                    on proizvodOpreme.id_oprema equals rezOprema.id_oprema
                    where rezOprema.id_rezervacije == id_rezervacije
                    select new DbItemOpremaSaParametrima()
                    {
                        cena = proizvodOpreme.cena,
                        id_oprema = proizvodOpreme.id_oprema,
                        id_tip_opreme = proizvodOpreme.id_tip_opreme,
                        kolicina_na_lageru = proizvodOpreme.kolicina_na_lageru,
                        model = proizvodOpreme.model,
                        naslov = proizvodOpreme.naslov,
                        proizvodjac = proizvodOpreme.proizvodjac,
                        slika = proizvodOpreme.slika,
                        kolicinaURezervacijama = rezOprema.kolicina_rezervisane_opreme
                    }
                    ).ToList();


            for (int i = 0; i < Lista.Count; i++)
            {
                Lista[i].ListaParametara = OpOpremaBase.VratiParametreZaOpremu(Lista[i].id_oprema, entities);
                Lista[i].slika = HttpContext.Current.Server.MapPath("." + Lista[i].slika).ToString();
            }
            return Lista;
        }





        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            List<DbItemRezervacijaSaOpremom> Lista = 
                (
                    from rezervacija in entities.rezervacijes
                    select new DbItemRezervacijaSaOpremom()
                    {
                        imeNaRezervacija = rezervacija.ime,
                        id_rezervacije = rezervacija.id_rezervacije,
                        datum_azuriranja_rezervacije = rezervacija.datum_azuriranja_rezervacije,
                        datum_isteka_rezervacije = rezervacija.datum_isteka_rezervacije,
                        datum_rezervacije = rezervacija.datum_rezervacije
                    }
                ).ToList();
            for (int i = 0; i < Lista.Count; i++)
            {
                Lista[i].ListaOpremeZaRezervaciju = this.vratiOpremuZaRezervacije(Lista[i].id_rezervacije, entities);
                
            }

            OperationObject opObj = new OperationObject();
            opObj.Niz = Lista.ToArray();
            opObj.Success = true;
            return opObj;
        }
    }
        public class OpRezervacijeInsert : OpRezervacijeBase
        {
            public DbItemOpremaSaParametrimaRezervacija[] DataSelectRezervacijaNiz { get; set; }

            public override OperationObject execute(SmartSoftwareBazaEntities entities)
            {
                System.Data.Objects.ObjectParameter idRezervacijePar = new System.Data.Objects.ObjectParameter("idRezervacije", System.Type.GetType("System.Int32"));
                
                 entities.DodajRezervacije(DataSelectOpremaRezervacija.imeNaRezervacija, DataSelectOpremaRezervacija.datum_rezervacije, DataSelectOpremaRezervacija.datum_isteka_rezervacije,idRezervacijePar );

                int id = Convert.ToInt32(idRezervacijePar.Value);


                foreach (var rezervacijaItem in DataSelectRezervacijaNiz)
                {
                    entities.InsertRezervacijeOprema(rezervacijaItem.id_oprema, id, rezervacijaItem.kolicina_rezervisane_opreme);
                }

                OperationObject opObj = new OperationObject();
                opObj.Success = true;
                return opObj;
            }
        }


        public class OpRezervacijeUpdate : OpRezervacijeBase
        {
            public override OperationObject execute(SmartSoftwareBazaEntities entities)
            {
                DataSelectOpremaRezervacija.datum_rezervacije = DateTime.Now;
                DataSelectOpremaRezervacija.datum_isteka_rezervacije = DateTime.Now;
                entities.AzurirajRezervacije(this.DataSelectOprema.id_oprema, this.DataSelectOpremaRezervacija.kolicina_rezervisane_opreme, DataSelectOpremaRezervacija.id_rezervacije, DataSelectOpremaRezervacija.datum_isteka_rezervacije, DataSelectOpremaRezervacija.datum_rezervacije);
                OperationObject opObj = new OperationObject();
                opObj.Success = true;
                return opObj;
            }
        }


        public class OpRezervacijeDelete : OpRezervacijeBase
        {
            public override OperationObject execute(SmartSoftwareBazaEntities entities)
            {
                OperationObject rez = null;
                if (this.DataSelectOpremaRezervacija != null)
                {
                    entities.ObrisiOpremuIzRezervacije(this.DataSelectOpremaRezervacija.id_oprema);
                    rez = base.execute(entities);
                }
                else
                {
                    entities.ObrisiSveRezervacije();
                    rez = new OperationObject() { Success = true };
                }
                return rez;
            }
        }
    [DataContract]
    public class DbItemOpremaSaParametrimaRezervacija:DbItemOpremaSaParametrima
    {
        [DataMember]
        public int id_rezervacije { get; set; }
        [DataMember]
        public string imeNaRezervacija { get; set; }
        [DataMember]
        public Nullable<System.DateTime> datum_rezervacije { get; set; }
        [DataMember]
        public Nullable<System.DateTime> datum_isteka_rezervacije { get; set; }
        [DataMember]
        public Nullable<System.DateTime> datum_azuriranja_rezervacije { get; set; }
        [DataMember]
        public int kolicina_rezervisane_opreme { get; set; }
    }
    [DataContract]
    public class DbItemRezervacijaSaOpremom
    {
        [DataMember]
        public int id_rezervacije { get; set; }
        [DataMember]
        public string imeNaRezervacija { get; set; }
        [DataMember]
        public System.DateTime? datum_rezervacije { get; set; }
        [DataMember]
        public System.DateTime? datum_isteka_rezervacije { get; set; }
        [DataMember]
        public System.DateTime? datum_azuriranja_rezervacije { get; set; }
        [DataMember]
        public List<DbItemOpremaSaParametrima> ListaOpremeZaRezervaciju { get; set; }
    }

}