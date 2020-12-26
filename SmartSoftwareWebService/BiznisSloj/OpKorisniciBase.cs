using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSoftwareWebService.BiznisSloj
{
    public abstract class OpKorisniciBase : Operation
    {
        public DbItemKorisnici KorisniciDataSelect { get; set; }

        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            DbItemKorisnici[] niz =
               (from korisnik in entities.korisnicis
                join uloga in entities.uloges
                on korisnik.id_uloge equals uloga.id_uloge
                select new DbItemKorisnici()
                {
                    broj_telefona = korisnik.broj_telefona,
                    id_korisnici = korisnik.id_korisnici,
                    ime = korisnik.ime,
                    prezime = korisnik.prezime,
                    lozinka = korisnik.lozinka,
                    brojOstvarenihPoena = korisnik.broj_ostvareni_poena,
                    mejl = korisnik.mejl,
                    id_uloge = korisnik.id_uloge,
                    naziv_uloge = uloga.naziv_uloge,
                    username = korisnik.username,
                    deletedField = korisnik.deletedField
                }).ToArray();
            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;
        }
    }

    public class OpKorisniciSelect : OpKorisniciBase
    {

    }

    public class OpKorisniciPretraga : OpKorisniciBase
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            DbItemKorisnici[] niz =
             (from korisnik in entities.korisnicis
              where korisnik.ime.Contains(KorisniciDataSelect.zaPretragu) 
              || korisnik.prezime.Contains(KorisniciDataSelect.zaPretragu)
              || korisnik.mejl.Contains(KorisniciDataSelect.zaPretragu)
              select new DbItemKorisnici()
              {
                  broj_telefona = korisnik.broj_telefona,
                  id_korisnici = korisnik.id_korisnici,
                  ime = korisnik.ime,
                  prezime = korisnik.prezime,
                  lozinka = korisnik.lozinka,
                  brojOstvarenihPoena = korisnik.broj_ostvareni_poena,
                  mejl = korisnik.mejl
              }).ToArray();
            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;
        }

    }

    public class OpKorisniciInsert : OpKorisniciBase
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            entities.DodajKorisnika(KorisniciDataSelect.ime, KorisniciDataSelect.prezime, KorisniciDataSelect.mejl, KorisniciDataSelect.broj_telefona, this.KorisniciDataSelect.id_uloge);
            return base.execute(entities);
        }
    }

    public class OpKorisniciAzurirajBrojPoena : OpKorisniciSelect
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            if (this.KorisniciDataSelect != null)
            {
                entities.AzurirajBrojPoena(this.KorisniciDataSelect.id_korisnici, this.KorisniciDataSelect.brojOstvarenihPoena);
            }
            OperationObject opObj = new OperationObject();
            opObj.Success = true;
            return opObj;
        }
    }

    public abstract class OpZaposleniKorisniciBase : Operation
    {
        public DbItemKorisnici ZaposleniKorisniciDataSelect { get; set; }

        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            DbItemKorisnici[] niz =
                (
                    from korisnik in entities.korisnicis
                    join uloga in entities.uloges
                    on korisnik.id_uloge equals uloga.id_uloge
                    where korisnik.id_uloge == 2 || korisnik.id_uloge == 1
                    select  new DbItemKorisnici()
                    {
                        id_korisnici = korisnik.id_korisnici,
                        ime = korisnik.ime,
                        prezime = korisnik.prezime,
                        broj_telefona = korisnik.broj_telefona,
                        mejl = korisnik.mejl,
                        brojOstvarenihPoena = korisnik.broj_ostvareni_poena,
                        lozinka = korisnik.lozinka,
                        username = korisnik.username,
                        naziv_uloge = uloga.naziv_uloge,
                        id_uloge = korisnik.id_uloge,
                        deletedField = korisnik.deletedField
                    }
                ).ToArray();
            //.GroupBy(p => p.id_korisnici).Select(g => g.First())
            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;
        }
    }

    public class OpZaposleniKorisniciSelect : OpZaposleniKorisniciBase
    {
        
    }

    public class OpZaposleniKorisniciInsert : OpZaposleniKorisniciBase
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            
            entities.ZaposleniKorisniciInsert(this.ZaposleniKorisniciDataSelect.ime, this.ZaposleniKorisniciDataSelect.prezime, this.ZaposleniKorisniciDataSelect.mejl, this.ZaposleniKorisniciDataSelect.broj_telefona, this.ZaposleniKorisniciDataSelect.username, this.ZaposleniKorisniciDataSelect.lozinka, this.ZaposleniKorisniciDataSelect.brojOstvarenihPoena, this.ZaposleniKorisniciDataSelect.id_uloge);

            return base.execute(entities);
        }
    }
    public class OpZaposleniKorisniciUpdate : OpZaposleniKorisniciBase
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            entities.ZaposleniKorisniciUpdate(this.ZaposleniKorisniciDataSelect.id_korisnici, this.ZaposleniKorisniciDataSelect.ime, this.ZaposleniKorisniciDataSelect.prezime, this.ZaposleniKorisniciDataSelect.mejl, this.ZaposleniKorisniciDataSelect.broj_telefona, this.ZaposleniKorisniciDataSelect.username, this.ZaposleniKorisniciDataSelect.lozinka, this.ZaposleniKorisniciDataSelect.brojOstvarenihPoena, this.ZaposleniKorisniciDataSelect.id_uloge);

            return base.execute(entities);
        }

    }


    public class OpKorisniciDelete : OpZaposleniKorisniciBase
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            entities.KorisniciDelete(this.ZaposleniKorisniciDataSelect.id_korisnici);
            return base.execute(entities);
        }  
    }
    public class OpKorisniciRestore : OpZaposleniKorisniciBase
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            entities.RestoreIzbrisanKorisnik(this.ZaposleniKorisniciDataSelect.id_korisnici);
            return base.execute(entities);
        }  
    }
}