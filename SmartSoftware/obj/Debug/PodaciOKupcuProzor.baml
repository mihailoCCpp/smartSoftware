   M S B A M L   `   `   `  ����  SmartSoftware&     SmartSoftware.PodaciOKupcuProzor   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   i eXceed.Wpf.AvalonDock.Themes.VS2010, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4g cXceed.Wpf.AvalonDock.Themes.Aero, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4h dXceed.Wpf.AvalonDock.Themes.Metro, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4[ WXceed.Wpf.AvalonDock, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4?avalon,http://schemas.xceed.com/wpf/xaml/avalondock     5   	     � Title$  PodaciOKupcuProzor��5   	   $	��500��6$   $	��700��61    � 
ResizeMode$ NoResize=�6=    � WindowStartupLocation$ CenterScreen=�6S   .��5      � .+�5   
   )  #Xceed.Wpf.AvalonDock.DockingManager     Layout. 5      ,  &Xceed.Wpf.AvalonDock.Layout.LayoutRoot     	RootPanel. 5      -  'Xceed.Wpf.AvalonDock.Layout.LayoutPanel  �  �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutPanelElement, Xceed.Wpf.AvalonDock, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]   Children. 5      9  3Xceed.Wpf.AvalonDock.Layout.LayoutDocumentPaneGroup  �  �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutDocumentPane, Xceed.Wpf.AvalonDock, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]   Children. 5      4  .Xceed.Wpf.AvalonDock.Layout.LayoutDocumentPane  �  �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.LayoutContent, Xceed.Wpf.AvalonDock, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]   Children. 5      0	  *Xceed.Wpf.AvalonDock.Layout.LayoutDocument	  /
  )Xceed.Wpf.AvalonDock.Layout.LayoutContent 
  Title$ Pretraga korisnika��64   	 
  CanClose	 .  
 
  CanFloat
 .   
  Content. 5   "   �  � RowDefinitions 5   &   �� 5   *   $��* �68   �� 5   *   $��2* �68    � ColumnDefinitions 5    &   �� 5!   *   $	��200 �6;   �� 5"   *   $	��500 �6;   .+�5$   &   � -   5        $��Pretraga��5$   H   $��0��6+   $��0��68   
�������.+�5%   *   � $	��175��6/    5&   .   �� 5'   2   $
��Auto �6@   �� 5(   2   $
��Auto �6@   �� 5)   2   $
��Auto �6@   .+�5+   .   �� $��Pretražite korisnika :��64   $��0��6U   �� 5,   .   -   5        $��1��5,   6   6D   �� 5-   .   $��2��65   $
��Auto��6B   $��Pretraži...��6P   $	��100��6f   $
��Left=�6r   $��0,10,0,0q�6�   5.   +   5/   '   � 50   &   -   5        $��PretragaInfo��50   Y   $��0��6+   $��1��68   $��2��6H   
�������.+�51   *   �� -   5        $��PrikazDetaljaKorisnika��51   2   
�������$	��500��6c   $��Hidden=�6o   ��53   .   �� 55   2   .R�56   6   � $	��500��6;   $��Center=�6G   
������� 58   :   �� 59   >   $��50 �6L   �� 5:   >   $
��Auto �6L   �� 5;   >   $
��Auto �6L   �� 5<   >   $
��Auto �6L   �� 5=   >   $
��Auto �6L   �� 5>   >   $��50 �6L   �� 5?   >   $
��Auto �6L    5A   :   �� 5B   >   $��2* �6O   �� 5C   >   $��20 �6O   �� 5D   >   $��3* �6O   .+�5F   :   �� $ ��PRIKAZ DETALJA O KORISNIKU��6@   $��3��6e   $��0��6y   $��Center=�6�   $��Center=�6�   �� 5G   :   $
��Ime:��6A   $��1��6P   $��0��6]   $��Right=�6m   �� 5H   :   $��Prezime:��6A   $��2��6T   $��0��6a   $��Right=�6q   �� 5I   :   $��e-mail:��6A   $��3��6S   $��0��6`   $��Right=�6p   �� 5J   :   $��Broj telefona:��6A   $��4��6Z   $��0��6g   $��Right=�6w   �� 5L   :   $��1��6c   $��2��6p   $
��Left=�6�   $��Center=�6�   ��6E   �� *ImeKorisnika+6�   �� 5M   :   $��2��6g   $��2��6t   $
��Left=�6�   $��Center=�6�   ��6E   �� *PrezimeKorisnika+6�   �� 5N   :   $��3��6d   $��2��6q   $
��Left=�6�   $��Center=�6�   ��6E   �� *MejlKorisnika+6�   �� 5O   :   $��4��6l   $��2��6y   $
��Left=�6�   $��Center=�6�   ��6E   �� *BrojTelefonaKorisnika+6�   �� 5P   :   $��btnIzaberiKorisnika��6  -   5        $��6��5P   A   $��0��6N   $��3��6^   $��Center=�6r   $��Center=�6�   $	��250��6�   $*��$Izaberite ovog korisnika za kupovinu��6�   
��� � �$��30��6�   
���������5Q   >   �� 5R   B    �� Color$ white��6R    J� Effect 5T   >   3  -System.Windows.Media.Effects.DropShadowEffect  5U   B      Color$ Black��6S      	Direction$	 -20Q�6a      
BlurRadius$ 15Q�6q      ShadowDepth$ 5Q�6�   5W   ;   5Y   7   5Z   3   5]   +   �� 5^   *   -   5        $��IzmenaDetaljaKorisnika��5^   2   $y�Hidden=�6P   ��5_   .   �� 5`   2   .R�5a   6   � $	��400��6;   $��Center=�6G    5c   :   �� 5d   >   $��50 �6L   �� 5e   >   $
��Auto �6L   �� 5f   >   $
��Auto �6L   �� 5g   >   $
��Auto �6L   �� 5h   >   $
��Auto �6L    5j   :   �� 5k   >   $��2* �6O   �� 5l   >   $��20 �6O   �� 5m   >   $��3* �6O   .+�5o   :   �� $ ��PRIKAZ DETALJA O KORISNIKU��6@   $��3��6e   $��0��6y   $��Center=�6�   $��Center=�6�   �� 5p   :   $
��Ime:��6A   $��1��6P   $��0��6]   $��Right=�6m   �� 5q   :   $��Prezime:��6A   $��2��6T   $��0��6a   $��Right=�6q   �� 5r   :   $��e-mail:��6A   $��3��6S   $��0��6`   $��Right=�6p   �� 5s   :   $��Broj telefona:��6A   $��4��6Z   $��0��6g   $��Right=�6w   �� 5u   :   $��1��6c   $��2��6p   $
��Left=�6�   $��Center=�6�   ��6E   �� *ImeKorisnika+6�   �� 5v   :   $��2��6g   $��2��6t   $
��Left=�6�   $��Center=�6�   ��6E   �� *PrezimeKorisnika+6�   �� 5w   :   $��3��6d   $��2��6q   $
��Left=�6�   $��Center=�6�   ��6E   �� *MejlKorisnika+6�   �� 5x   :   $��4��6l   $��2��6y   $
��Left=�6�   $��Center=�6�   ��6E   �� *BrojTelefonaKorisnika+6�   5y   7   5z   3   5}   +   5~   '   � 5   &   $��1��6+   $��0��68   .+�5�   *   �� -   5        $��PrikazRezultataKorisnika��5�   3   $	��200��6S   $ �� IsSynchronizedWithCurrentItem$
 TrueY�6_   $��Hidden=�6�   $��0,0,5,0q�6�   ��6�   �� *ListaKorisnika+��5�   .   �� 5�   2   .R�5�   :   � $"��GridPrikazRezultataKorisnika��6?   -   5         5�   >   �� 5�   B   $
��Auto �6P   �� 5�   B   $
��Auto �6P    5�   >   �� 5�   >   $
��Auto �6O   �� 5�   >   $
��Auto �6O   .+�5�   >   �� $��	skrivenId��6y   $y�Hidden=�6H   ��6\   �� *IdKorisnici+6y   �� 5�   >   $��0��6H   $��0��6U   $��Center=�6�   $��Right=�6�    �� Padding$ 0,0,15,0q�6�   ��6e   �� *ImeKorisnika+6�   �� 5�   >   $��0��6H   $��1��6U   $
��Left=�6�   $��Center=�6�   $	��200��6�   ��6e   �� *PrezimeKorisnika+6�   �� 5�   >   $��1��6H   $��0��6U   $��2��6e   $��Italic$�6�   $��10(�6�   
���������6y   �� *MejlKorisnika+6�   5�   3   5�   3   5�   +   5�   '   5�   #   5�      	  5�      $ Registracija korisnika��64   	 .  
 .  . 5�   "   � .+�5�   &   � $	��400��6+   $��Center=�67    5�   *   �� 5�   .   $��50 �6<   �� 5�   .   $
��Auto �6<   �� 5�   .   $
��Auto �6<   �� 5�   .   $
��Auto �6<   �� 5�   .   $
��Auto �6<   �� 5�   .   $��50 �6<   �� 5�   .   $
��Auto �6<    5�   *   �� 5�   .   $��2* �6?   �� 5�   .   $��20 �6?   �� 5�   .   $��3* �6?   .+�5�   *   �� $&�� POPUNJAVANJE DETALJA O KORISNIKU��60   $��3��6[   $��0��6o   $��Center=�6|   $��Center=�6�   �� 5�   *   $
��Ime:��61   $��1��6@   $��0��6M   $��Right=�6]   �� 5�   *   $��Prezime:��61   $��2��6D   $��0��6Q   $��Right=�6a   �� 5�   *   $��e-mail:��61   $��3��6C   $��0��6P   $��Right=�6`   �� 5�   *   $��Broj telefona:��61   $��4��6J   $��0��6W   $��Right=�6g   �� 5�   *   $��1��6\   $��2��6i   $
��Left=�6y   $��Center=�6�   $	��150��6�   ��62   �� *TmpKorisnik.ImeKorisnika+6�   �� 5�   *   $��2��6`   $��2��6m   $
��Left=�6}   $��Center=�6�   $	��150��6�   ��62   �� *TmpKorisnik.PrezimeKorisnika+6�   �� 5�   *   $��3��6]   $��2��6j   $
��Left=�6z   $��Center=�6�   $	��150��6�   ��62   �� *TmpKorisnik.MejlKorisnika+6�   �� 5�   *   $��4��6e   $��2��6r   $
��Left=�6�   $��Center=�6�   $	��150��6�   ��62   �� *#!TmpKorisnik.BrojTelefonaKorisnika+6�   �� 5�   *   -	   5        $��btnRegistracijaKorisnika��5�   �   $��6��61   $��0��6>   $��3��6N   $��Center=�6b   $��Center=�6   $	��150��6�   $��Registruj Korisnika��6�   
���**��
���������5�   .   �� 5�   2   $ white��6B    5�   .     5�   2   $ Black��6C   $	 -20Q�6Q   $ 15Q�6a   $ 5Q�6q   5�   +   5�   '   5�   #   5�      5�      5�      5�      5�      5�      5�      5�      