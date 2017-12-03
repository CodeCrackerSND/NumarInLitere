/*
 * Created by SharpDevelop.
 * User: Mihai
 * Date: 11/30/2017
 * Time: 2:41 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace conversie
{
class Conversie
{
         
         public static int NumarDeCifre(int numar)
         {
         
         	int NumarDeCifre = 0;
         	int numar_lucru = numar;
         	while (numar_lucru!=0)
         	{
         	numar_lucru /= 10; // impartim la zece
         	NumarDeCifre++;
         	}
         	
         	if (numar==0) NumarDeCifre++;  // avem o exceptie: cand numarul este 0 avem o cifra
         	
         	return NumarDeCifre;
         	
         }
         
         public static int RidicareLaPutere(int numar, int putere)
         {
         	
            int calculat = 1;
         	while (putere!=0)
         	{
         	calculat *= 10; // inmultim cu zece
			putere--;
         	}
         	
         	return calculat;
         	
         }
         
         public static int CifraDeLaPozitie(int numar, int pozitie)
         {
            pozitie--;
            int divizor = RidicareLaPutere(10, pozitie);
         	return numar/divizor;  // imparte pentru a obtine cifra de la pozitie

         }
         
static string[] cifre_lit = new string[] { "o", "doua", "trei", "patru", "cinci", "sase", "sapte", "opt", "noua" };
static string[] cifre_lit2 = new string[] {"zece", "unsprezece", "doisprezece", "treisprezece", "paisprezece", "cincisprezece", "saisprezece", "saptesprezece", "optusprezece", "nouasprezece" };

         public static string CifraLaLiteral(int cifra, int numar_de_cifre, bool EsteFeminin)
         {
         	if (cifra==0) return "zero";
         	
         	if ((cifra>=10)&&((cifra-10)<cifre_lit2.Length))  // numar din doua litere!
         	return cifre_lit2[cifra-10];
         	
         	if (cifra==6&&(numar_de_cifre==2||numar_de_cifre==5||numar_de_cifre==8))  // 64 - sai nu sase
         	return "sai";  // sai nu sase
      
      switch (cifra)
      {
          case 1:
         	if (EsteFeminin)  // miliarde este un miliard - masculin la singular
         	return "o";
         	else
         	return "un";

          case 2:
         	if (EsteFeminin)
         	return "doua";
         	else
         	return "doi";
         	         	
          default:
         	if ((cifra>=0)&&((cifra-1)<cifre_lit.Length))
         		return cifre_lit[cifra-1];
         	else
         		return "";

      }
      
		
        
         }
         
         public static string NumarDeCifreInDeCe(int numar_de_cifre, bool EsteFeminin, bool bool_EstePlural, bool EraGol)
         {
         	
              if (numar_de_cifre==10)  // miliarde
              {
              if (bool_EstePlural)
              return "miliarde ";
              else
              return "miliard ";
              }
              
              if (numar_de_cifre==9||numar_de_cifre==6||numar_de_cifre==3) // sute de milioane || sute de mii || sute
              {
              if (bool_EstePlural)
              return "sute ";
              else
              return "suta ";
              }
              

              if (numar_de_cifre==8||numar_de_cifre==7)
              {
              // if (!EraGol) construit += " de";
              if (bool_EstePlural)
              return "milioane ";
              else
              return "milion ";
              }
              
              if (numar_de_cifre==5||numar_de_cifre==4)
              {
              // if (!EraGol) construit += " de";
              if (bool_EstePlural)
              return "mii ";
              else
              return "mie ";
              }
              
              if (numar_de_cifre==2)  // nu a mai ramas nimic
              return " ";
                            
              if (numar_de_cifre==1&&(!bool_EstePlural))  // si "numar"
              {
              return "u";  // un + u = unu
              }
              
              return "";
              
         }
         
         
         public static bool EstePlural(int cifra)
         {
         	if (cifra>1)
         	return true;
         	else
         	return false;
         }
         
         public static string Separator = "";
         
         public static string ConversieNumarIntreg(int numar)
         {
         	if (numar==0) return "zero";
         	
         	bool SpecialAvemMilioane = false;
         	bool SpecialAvemMii = false;
         	string construit = "";
			int numar_de_cifre = NumarDeCifre(numar);
			while (numar_de_cifre!=0)
			{
			int numar_de_la_pozitie = CifraDeLaPozitie(numar, numar_de_cifre);
			
		if (numar_de_la_pozitie>0||(SpecialAvemMilioane&&numar_de_cifre==7)||(SpecialAvemMii&&numar_de_cifre==4))
		{		// cifrele cu zero nu ne intereseaza
	 bool EsteFeminin = false;
	 bool bool_EstePlural = EstePlural(numar_de_la_pozitie);
      switch (numar_de_cifre)
      {
          case 10:  // miliarde
          case 9:  // sute de milioane
          case 6:  // sute de mii
          case 3:  // sute
      	  
      		if (construit!="")
      	    construit += Separator;
      	      
      		if (numar_de_cifre==9&&numar_de_la_pozitie>0)
      		SpecialAvemMilioane = true; // avem miloane
      		
      		if (numar_de_cifre==6&&numar_de_la_pozitie>0)
      		SpecialAvemMii = true; // avem mii
      		
      		if (numar_de_cifre==10)  // miliard la singular este un
      		  EsteFeminin = false;
      		else
      		  EsteFeminin = true;
      		
              construit += CifraLaLiteral(numar_de_la_pozitie,numar_de_cifre, EsteFeminin)+" ";
              construit += NumarDeCifreInDeCe(numar_de_cifre, EsteFeminin, bool_EstePlural, false);
      	    

              break;
              
          case 8:  //  daca este 1 prima cifra: sprezece, altfel doua zeci de milioane
          case 5:  //  daca este 1 prima cifra: sprezece, altfel zeci de mii
          case 2:  //  daca este 1 prima cifra: sprezece, altfel zeci
              
              if (numar_de_cifre==8&&numar_de_la_pozitie>0)
      		  SpecialAvemMilioane = false; // avem milioane
              
              if (numar_de_cifre==5&&numar_de_la_pozitie>0)
      		  SpecialAvemMii = false; // avem mii
                            
      		  EsteFeminin = true;
      		  if (numar_de_la_pozitie==1)
      		  {  // iau doua cifre acum
              int numar_de_cifre_vechi = numar_de_cifre;
      		  numar_de_cifre--;
      		  numar_de_la_pozitie = CifraDeLaPozitie(numar, numar_de_cifre);  //  int doua_numere_de_la_pozitie
      		  if (numar_de_la_pozitie>1)
      		  bool_EstePlural = true;
      		  
              construit += CifraLaLiteral(numar_de_la_pozitie, numar_de_cifre, EsteFeminin)+" ";
              construit += NumarDeCifreInDeCe(numar_de_cifre_vechi, EsteFeminin, bool_EstePlural, false);
      		  }
      		  else
      		  {
              construit += CifraLaLiteral(numar_de_la_pozitie,numar_de_cifre, EsteFeminin)+" ";
              if (bool_EstePlural)
              construit += "zeci";
      		  }
              construit += " ";
              break;
              
          case 7:  // un numar singur urmat: "si numar de milioane"
          case 4:  // si "numar" de mii
              
            if (numar_de_cifre==7&&SpecialAvemMilioane&&numar_de_la_pozitie==0)
      		{
      		construit += NumarDeCifreInDeCe(numar_de_cifre, EsteFeminin, true, false);
      		SpecialAvemMilioane = false;
      		break;
      		}
            
            if (numar_de_cifre==4&&SpecialAvemMii&&numar_de_la_pozitie==0)
      		{
      		construit += NumarDeCifreInDeCe(numar_de_cifre, EsteFeminin, true, false);
      		SpecialAvemMii = false;
      		break;
      		}

              
      		  EsteFeminin = false;
              if (bool_EstePlural)
      		  EsteFeminin = true;
              else if (numar_de_cifre==7)
      		  EsteFeminin = false;
              else if (numar_de_cifre==4)
      		  EsteFeminin = true;
              
              bool EraGol = construit=="";
              
      		  if (!EraGol)
      		  construit += "si ";
      		  
              construit += CifraLaLiteral(numar_de_la_pozitie,numar_de_cifre, EsteFeminin)+" ";
              construit += NumarDeCifreInDeCe(numar_de_cifre, EsteFeminin, bool_EstePlural, EraGol);
              break;
              
             case 1:  // ultima cifra:
      		  if (construit!="")  // daca nu era gol
      		  construit += "si ";
      		  
              construit += CifraLaLiteral(numar_de_la_pozitie,numar_de_cifre, EsteFeminin)+" ";
              break;
              
           default:
              break;
      }
      
		}
			
      		int ridicat = 1;
			
			if ((numar_de_cifre-1)>0)  // verifica ca puterea sa fie mai mare ca zero
			ridicat = RidicareLaPutere(10, numar_de_cifre-1);
			
			int de_scazut = numar_de_la_pozitie*ridicat;  // eliminam numarul care a fost deja printat
			numar = numar-de_scazut;
			
			numar_de_cifre--;
			
			}
			
			return construit;
         
         }
         
         public static string SeparatorUnitati = "si";
         public static string PrimaUnitate = "leu";
         public static string PrimaUnitatePlural;
         public static string ADouaUnitate = "ban";
         public static string ADouaUnitatePlural;
         public static bool AreBani = false;
         
         public static string ConversieNumarDouble(double numar)
         {
         int parteIntreaga = (int)numar;
         // int parteFractional = (int)((numar - parteIntreaga) * 100);
         int first2DecimalPlaces = (int)(((decimal)numar % 1) * 100);
         if (first2DecimalPlaces==0)
         AreBani = false;
         else
         AreBani = true;
         
         string convertit = "";
         
         PrimaUnitatePlural = PrimaUnitate;
         if (PrimaUnitatePlural.EndsWith("u"))
         PrimaUnitatePlural = PrimaUnitatePlural.Substring(0, PrimaUnitatePlural.Length-1);
         PrimaUnitatePlural += "i";
         	
         ADouaUnitatePlural = ADouaUnitate+"i";
         
         if (parteIntreaga<0)  // dace e numar negativ
         {
         convertit += "minus ";
         parteIntreaga = -parteIntreaga;
         }
         convertit += ConversieNumarIntreg(parteIntreaga);
         if (parteIntreaga==1)
         convertit += PrimaUnitate+" ";
         else
         convertit += PrimaUnitatePlural+" ";
         
         convertit += SeparatorUnitati+" "+ConversieNumarIntreg(first2DecimalPlaces);
         
         if (first2DecimalPlaces==1)
         convertit += ADouaUnitate+" ";
         else
         convertit += ADouaUnitatePlural+" ";
         
         return convertit;
         }
         
         public static char SepatorZecimale = '.';
         
         public static string ConversieNumarDouble(string numar_double)
         {
         string[] splited = numar_double.Split(SepatorZecimale);
         int parteIntreaga = 0;
         
        // ToInt32 can throw FormatException or OverflowException.
        try
        {
        parteIntreaga = Convert.ToInt32(splited[0]);
        }
        catch (FormatException e)
        {

        }

         // int parteFractional = (int)((numar - parteIntreaga) * 100);
         int first2DecimalPlaces = 0;
         if (splited.Length>0)
         {
         if (splited[1].Length<2) splited[1] += "0";
         
         splited[1] = splited[1].Substring(0, 2);
         
        try
        {
        first2DecimalPlaces = Convert.ToInt32(splited[1]);
        }
        catch (FormatException e)
        {

        }
         }

         if (first2DecimalPlaces==0)
         AreBani = false;
         else
         AreBani = true;
         
         string convertit = "";
         
         PrimaUnitatePlural = PrimaUnitate;
         if (PrimaUnitatePlural.EndsWith("u"))
         PrimaUnitatePlural = PrimaUnitatePlural.Substring(0, PrimaUnitatePlural.Length-1);
         PrimaUnitatePlural += "i";
         	
         ADouaUnitatePlural = ADouaUnitate+"i";
         
         if (parteIntreaga<0)  // dace e numar negativ
         {
         convertit += "minus ";
         parteIntreaga = -parteIntreaga;
         }
         convertit += ConversieNumarIntreg(parteIntreaga);
         if (parteIntreaga==1)
         convertit += PrimaUnitate+" ";
         else
         convertit += PrimaUnitatePlural+" ";
         
         convertit += SeparatorUnitati+" "+ConversieNumarIntreg(first2DecimalPlaces);
         
         if (first2DecimalPlaces==1)
         convertit += ADouaUnitate+" ";
         else
         convertit += ADouaUnitatePlural+" ";
         
         return convertit;
         }
         
         
         
		public static void Main(string[] args)
		{
			string str1 = ConversieNumarDouble("464265246.25");
			string str = ConversieNumarIntreg((int)11
			                                 );
			// (float)101.08 = o suta zece Lei  si optzeci Bani
			
			
			Console.WriteLine("Hello World!");
			
			// TODO: Implement Functionality Here
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
}

}



