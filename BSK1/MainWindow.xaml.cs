using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Numerics;
using System.IO;
using System.Collections;

namespace BSK1
{

    public partial class MainWindow : Window
    {
        private int[] finalkey;
        public MainWindow()
        {
            InitializeComponent();
        }
        #region Zadanie 1
        //przycisk szyfrowania
        private void szyfruj_Zadanie1_Click(object sender, RoutedEventArgs e)
        {
            int n = int.Parse(textN.Text);  //konwersja text na int
            string slowo = textSlowo.Text.ToString(); //konwersja text na string
            textZaszyfrowaneSlowo.Text = szyfrowaniePlotkowe(slowo, n); //szyfrowanie słowa i wypisanie na kontrolkę 
        }
        //przycisk deszyfrowania
        private void odszyfruj_Zadanie1_Click(object sender, RoutedEventArgs e)
        {
            int n = int.Parse(textN2.Text);
            string slowo = textSlowo2.Text.ToString();
            textOdszyfrowaneSlowo.Text = deszyfrowaniePlotkowe(slowo, n);//odszyfrowanie słowa i wypisanie
        }

        //metoda szyfrująca moduł Rail fence
        private string szyfrowaniePlotkowe(string slowo, int n)
        {
            string zaszyfrowane = null; //słowo po zaszyfrowaniu
            int tabResultIndex = 0; //obecny indeks
            char[] TabResult = new char[slowo.Length]; //tablica wyniku

            List<int> index = new List<int>(); // lista indexów wierzchołków.

            // wierzchołki górne 2*(n-1)  "pierwszy wiersz" - te znaki będą rozpoczynały szyfr
            for (int i = 0; i < slowo.Length; i++)
            {
                if (2 * (n - 1) * i <= slowo.Length - 1)
                {
                    TabResult[tabResultIndex] = slowo[2 * (n - 1) * i];
                    tabResultIndex++;
                    index.Add(2 * (n - 1) * i); //zapisanie indeksu do tablicy 
                }
            }

            // dodatkowy wierzchołek 
            int dindex = index.Last();
            dindex = dindex + (2 * (n - 1));
            index.Add(dindex);

            // srodkowe wiersze (od drugiego do przedostatniego)
            for (int i = 1; i < n - 1; i++)
            {
                foreach (var item in index)
                {
                    if ((item - i <= slowo.Length - 1) && (item - i >= 0))
                    {
                        TabResult[tabResultIndex] = slowo[item - i];
                        tabResultIndex++;
                    }
                    if (item + i <= slowo.Length - 1)
                    {
                        TabResult[tabResultIndex] = slowo[item + i];
                        tabResultIndex++;
                    }
                }
            }
            // wierzcholki dolne (ostatni wiersz)

            for (int i = 0; i < slowo.Length; i++)
            {
                if (2 * (n - 1) * i + (n - 1) <= slowo.Length - 1)
                {
                    TabResult[tabResultIndex] = slowo[(2 * (n - 1) * i + (n - 1))];
                    tabResultIndex++;
                }
            }

            foreach (var item in TabResult) //sklejenie wszystkich wartości z tablicy  do Stringa
            {
                zaszyfrowane += item;
            }
            return zaszyfrowane;
        }

        //metoda deszyfrująca moduł Rail Fence
        private string deszyfrowaniePlotkowe(string slowo, int n)
        {
            string odszyfrowane = null;
            int tabResultIndex = 0; //obecny indeks
            char[] TabResult = new char[slowo.Length];
            List<int> index = new List<int>(); // tablica indexów wierzchołków górnych

            // wierzcholki górne
            for (int i = 0; i < slowo.Length; i++)
            {
                if (2 * (n - 1) * i <= slowo.Length - 1)
                {
                    TabResult[(2 * (n - 1) * i)] = slowo[tabResultIndex];
                    tabResultIndex++;
                    index.Add(2 * (n - 1) * i);
                }

            }

            // dodatkowy wierzchołek.
            int dindex = index.Last();
            dindex = dindex + (2 * (n - 1));
            index.Add(dindex);

            for (int i = 1; i < n - 1; i++)
            {
                foreach (var item in index)
                {
                    if (!(item - i > slowo.Length - 1) && (item - i >= 0))
                    {
                        TabResult[(item - i)] = slowo[tabResultIndex];
                        tabResultIndex++;
                    }
                    if (!(item + i > slowo.Length - 1))
                    {
                        TabResult[(item + i)] = slowo[tabResultIndex];
                        tabResultIndex++;
                    }
                }
            }
            for (int i = 0; i < slowo.Length; i++)
            {
                if (!(2 * (n - 1) * i + (n - 1) > slowo.Length - 1))
                {
                    TabResult[(2 * (n - 1) * i + (n - 1))] = slowo[tabResultIndex];
                    tabResultIndex++;
                }
            }

            foreach (var item in TabResult)
            {
                odszyfrowane += item;
            }

            return odszyfrowane;
        }


        #endregion

        #region Zadanie2a
        private void button3_Copy_Click(object sender, RoutedEventArgs e)
        {

            int[] key = new[] { 3, 5, 1, 2, 4 };
            string slowo = textBox6_Copy1.Text.ToString();
            char[] decodedWord = new char[slowo.Length];
            string decodeslowo = null;
            int[] pointers = (int[])key.Clone();

            int mainPointer = 0;

            for (int i = 0; i < Math.Ceiling(slowo.Length / (float)key.Length); i++)
            {
                foreach (int pointer in pointers)
                {
                    if (pointer <= slowo.Length)
                    {
                        decodedWord[pointer - 1] = slowo[mainPointer++];

                    }

                }
                for (int j = 0; j < pointers.Length; j++)
                {
                    pointers[j] += pointers.Length;
                }

            }
            foreach (var item in decodedWord)
            {
                decodeslowo += item;
            }




            textBox6_Copy2.Text = decodeslowo;
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            string szyfrowanko = null;
            int[] key = new[] { 3, 4, 1, 5, 2 };
            string slowo = textBox6.Text.ToString();

            char[] encodeslowo = new char[slowo.Length];
            int[] pointers = (int[])key.Clone();

            int mainPointer = 0;

            for (int i = 0; i < Math.Ceiling(slowo.Length / (float)key.Length); i++)
            {
                foreach (int pointer in pointers)
                {
                    if (pointer <= slowo.Length)
                    {
                        encodeslowo[pointer - 1] = slowo[mainPointer++];
                    }
                }
                for (int j = 0; j < pointers.Length; j++)
                {
                    pointers[j] += pointers.Length;
                }


            }
            foreach (var item in encodeslowo)
            {
                szyfrowanko += item;
            }



            textBox6_Copy.Text = szyfrowanko;
        }
        #endregion

        #region Zadanie2b

        private void szyfrowanie2b_Click(object sender, RoutedEventArgs e)
        {
            char[] alfabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            string wynik = null;
            char[] key = text2bklucz.Text.ToCharArray();
            int[] kluczglowny = new int[key.Length];
            string haslo = text2bhaslo.Text;
            char[] temp = haslo.ToCharArray();
            int pozycja = 0;

            //tworzenia klucza jak w 2a
            for (int i = 0; i < alfabet.Length; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (alfabet[i] == key[j])
                    {
                        kluczglowny[j] = pozycja;
                        pozycja++;
                    }

                }
            }
            finalkey = kluczglowny;
            //kodowanko
            char[] encodeslowo = new char[haslo.Length];
            int[] pointers = (int[])kluczglowny.Clone();

            int mainPointer = 0;

            for (int j = 0; j < pointers.Length; j++)
            {
                while (pointers[j] < haslo.Length)
                {
                    encodeslowo[mainPointer++] = haslo[pointers[j]];
                    pointers[j] += pointers.Length;
                }
            }



            foreach (var item in encodeslowo)
            {
                wynik += item;
            }



            text2bzaszyfrowane.Text = wynik;

        }
        private void Odszyfrowanie2b_Click(object sender, RoutedEventArgs e)
        {
            char[] alfabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            string wynik = null;
            char[] key = text2bklucz.Text.ToCharArray();
            int[] kluczglowny = new int[key.Length];
            // int[] keytable = new int[key.Length];
            string haslo = text2bhaslodoodszyfrowania.Text;
            char[] temp = haslo.ToCharArray();




            char[] decodedWord = new char[haslo.Length];

            int[] pointers = (int[])finalkey.Clone();

            int mainPointer = 0;

            for (int j = 0; j < pointers.Length; j++)
            {
                while (pointers[j] < haslo.Length)
                {

                    decodedWord[pointers[j]] = haslo[mainPointer++];
                    pointers[j] += pointers.Length;
                }

            }
            foreach (var item in decodedWord)
            {
                wynik += item;
            }




            text2bpoodszyfrowaniu.Text = wynik;
        }

        #endregion

        #region Zadanie 4   
        private void button_Click(object sender, RoutedEventArgs e)
        {
            char[] alfabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(); // 26
            int k_0 = Int32.Parse(textk0.Text);
            int k_1 = Int32.Parse(textk1.Text);
            string tekst_odszyfrowany = null;
            char[] do_zaszyfrowania = textBox.Text.ToCharArray();

            for (int i = 0; i < do_zaszyfrowania.Length; i++)
            {
                int a = Check_index(do_zaszyfrowania[i]);
                tekst_odszyfrowany += alfabet[((a * k_1) + k_0) % alfabet.Length];

            }
            textBox3.Text = tekst_odszyfrowany;
        }
        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            char[] alfabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(); // 26
            int k_0 = Int32.Parse(textk0.Text);
            int k_1 = Int32.Parse(textk1.Text);



            string tekst_odszyfrowany = null;

            char[] do_zaszyfrowania = textBox4_Copy.Text.ToCharArray();

            for (int i = 0; i < do_zaszyfrowania.Length; i++)
            {
                int a = Check_index(do_zaszyfrowania[i]);
                BigInteger potega = BigInteger.ModPow(k_1, 11, 26);


                BigInteger wzor_bez_modulo = (a + (26 - k_0)) * potega;

                BigInteger b = modulo_dodatnie(wzor_bez_modulo, 26);
                tekst_odszyfrowany += alfabet[(int)b];

            }

            textBox4.Text = tekst_odszyfrowany;



        }

        private BigInteger modulo_dodatnie(BigInteger liczba, int modulo)
        {

            BigInteger r = liczba % modulo;
            return r < 0 ? r + modulo : r;
        }

        #endregion
        #region zadanie 5
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            char[] alfabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            char[] do_zaszyfrowani = textBox1.Text.ToCharArray();
            char[] key = textBox2.Text.ToCharArray();
            string szyfrowanko = null;
            char[] keyTable = new char[do_zaszyfrowani.Length];

            for (int i = 0; i < do_zaszyfrowani.Length; i++)
            {
                int j = 0;
                szyfrowanko += alfabet[(Check_index(do_zaszyfrowani[i]) + Check_index(key[j])) % alfabet.Length];
                j = (j + 1) % key.Length;
            }
            textBox5.Text = szyfrowanko;
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            char[] alfabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            char[] do_zaszyfrowani = textBox2_Copy.Text.ToCharArray();
            char[] key = textBox2.Text.ToCharArray();
            string szyfrowanko = null;
            char[] keyTable = new char[do_zaszyfrowani.Length];

            for (int i = 0; i < do_zaszyfrowani.Length; i++)
            {
                int j = 0;
                szyfrowanko += alfabet[(Check_index(do_zaszyfrowani[i]) - Check_index(key[j]) + alfabet.Length) % alfabet.Length];
                j = (j + 1) % key.Length;
            }
            textBox5_Copy.Text = szyfrowanko;
        }
        #endregion






        private int Check_index(char znak)
        {
            char[] Alfabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            for (int i = 0; i < Alfabet.Length; i++)
            {
                if (Alfabet[i] == znak)
                    return i;
            }
            return 0;
        }

        #region lsfr
        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            history.Text = "";
            int liczba_przesuniec = Int32.Parse(repeats.Text);
            int[] wyjscie = new int[liczba_przesuniec];
            int suma = 0;
            string seed_str = lsfr_seed.Text;
            string poly_str = lsfr_polynomial.Text;
            int[] seed = new int[seed_str.Length];
            int[] polynomial = new int[poly_str.Length];

            for (int i = 0; i < seed_str.Length; i++)
            {
                seed[i] = seed_str[i] - '0';
            }
            for (int i = 0; i < poly_str.Length; i++)
            {
                polynomial[i] = poly_str[i] - '0';
            }

            int[] new_seed = new int[seed.Length];



            for (int i = 0; i < liczba_przesuniec; i++)
            {
                for (int j = 0; j < seed.Length; j++)
                {
                    if (polynomial[j] == 1)
                    {
                        suma = suma + seed[j];
                    }

                }

                for (int j = 0; j < (seed.Length - 1); j++)
                {
                    new_seed[j + 1] = seed[j];
                }
                new_seed[0] = suma % 2;
                for (int j = 0; j < new_seed.Length; j++)
                {
                    seed[j] = new_seed[j];
                }
                history.Text = history.Text + String.Join("", seed) + "\n";
                suma = 0;
                wyjscie[i] = seed[0];
            }


            result.Text = string.Join("", wyjscie);

        }
        #endregion

        private void Converting_Click(object sender, RoutedEventArgs e)
        {
            string inputFilename = @"C:\elo\test.bin";

            byte[] fileBytes = File.ReadAllBytes(inputFilename);
            string strBin = string.Empty;
            byte btindx = 0;
            string strAllbin = string.Empty;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < fileBytes.Length; i++)
            {
                btindx = fileBytes[i];

                strBin = Convert.ToString(btindx, 2); // Convert from Byte to Bin
                strBin = strBin.PadLeft(8, '0');  // Zero Pad

                sb.Append(strBin);
            }
            strAllbin = sb.ToString();

            int[] source = new int[strAllbin.Length];

            int k = 0;

            foreach (char c in strAllbin)
            {
                source[k++] = Convert.ToInt32(c);
            }

            for (int i = 0; i < source.Length; i++)
            {
                source[i] = source[i] - 48;
            }

            //GOTOWA TABLICA INTOW 






            int liczba_przesuniec = source.Length;
            int[] wyjscie = new int[liczba_przesuniec];
            int suma = 0;
            string seed_str = lsfr_seed2.Text;
            string poly_str = lsfr_polynomial2.Text;


            int[] seed = new int[seed_str.Length];
            int[] polynomial = new int[poly_str.Length];

            for (int i = 0; i < seed_str.Length; i++)
            {
                seed[i] = seed_str[i] - '0';
            }
            for (int i = 0; i < poly_str.Length; i++)
            {
                polynomial[i] = poly_str[i] - '0';
            }

            int[] new_seed = new int[seed.Length];



            for (int i = 0; i < liczba_przesuniec; i++)
            {
                for (int j = 0; j < seed.Length; j++)
                {
                    if (polynomial[j] == 1)
                    {
                        suma = suma + seed[j];
                    }

                }

                for (int j = 0; j < (seed.Length - 1); j++)
                {
                    new_seed[j + 1] = seed[j];
                }
                new_seed[0] = suma % 2;
                for (int j = 0; j < new_seed.Length; j++)
                {
                    seed[j] = new_seed[j];
                }

                suma = 0;
                wyjscie[i] = seed[0];
            }
            int[] wynik = new int[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                wynik[i] = (source[i] + wyjscie[i]) % 2;
            }

            byte byteOutput = 0;
            BinaryWriter bw = new BinaryWriter(new FileStream(@"C:\elo\testoutput.bin", FileMode.Create));

            for (int i = 0; i < wynik.Length; i += 8)
            {
                byteOutput = Convert.ToByte(128 * wynik[i] + 64 * wynik[i + 1] + 32 * wynik[i + 2] + 16 * wynik[i + 3] + 8 * wynik[i + 4] + 4 * wynik[i + 5] + 2 * wynik[i + 6] + wynik[i + 7]);
                bw.Write(byteOutput);
                byteOutput = 0;


            }
            bw.Close();
            doneCoding.Text = "Zakodowano";
        }


        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            string inputFilename = @"C:\elo\testoutput.bin";
            byte[] fileBytes = File.ReadAllBytes(inputFilename);
            string strBin = string.Empty;
            byte btindx = 0;
            string strAllbin = string.Empty;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < fileBytes.Length; i++)
            {
                btindx = fileBytes[i];

                strBin = Convert.ToString(btindx, 2); // Convert from Byte to Bin
                strBin = strBin.PadLeft(8, '0');  // Zero Pad

                sb.Append(strBin);
            }
            strAllbin = sb.ToString();

            int[] source = new int[strAllbin.Length];

            int k = 0;

            foreach (char c in strAllbin)
            {
                source[k++] = Convert.ToInt32(c);
            }

            for (int i = 0; i < source.Length; i++)
            {
                source[i] = source[i] - 48;
            }

            //gotowa tablica intow


            int liczba_przesuniec = source.Length;
            int[] wyjscie = new int[liczba_przesuniec];
            int suma = 0;
            string seed_str = lsfr_seed2.Text;
            string poly_str = lsfr_polynomial2.Text;


            int[] seed = new int[seed_str.Length];
            int[] polynomial = new int[poly_str.Length];

            for (int i = 0; i < seed_str.Length; i++)
            {
                seed[i] = seed_str[i] - '0';
            }
            for (int i = 0; i < poly_str.Length; i++)
            {
                polynomial[i] = poly_str[i] - '0';
            }

            int[] new_seed = new int[seed.Length];



            for (int i = 0; i < liczba_przesuniec; i++)
            {
                for (int j = 0; j < seed.Length; j++)
                {
                    if (polynomial[j] == 1)
                    {
                        suma = suma + seed[j];
                    }

                }

                for (int j = 0; j < (seed.Length - 1); j++)
                {
                    new_seed[j + 1] = seed[j];
                }
                new_seed[0] = suma % 2;
                for (int j = 0; j < new_seed.Length; j++)
                {
                    seed[j] = new_seed[j];
                }

                suma = 0;
                wyjscie[i] = seed[0];
            }
            int[] wynik = new int[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                wynik[i] = (source[i] + wyjscie[i]) % 2;
            }

            BinaryWriter bw = new BinaryWriter(new FileStream(@"C:\elo\testDeconverted.bin", FileMode.Create));

            byte byteOutput = 0;


            for (int i = 0; i < wynik.Length; i += 8)
            {
                byteOutput = Convert.ToByte(128 * wynik[i] + 64 * wynik[i + 1] + 32 * wynik[i + 2] + 16 * wynik[i + 3] + 8 * wynik[i + 4] + 4 * wynik[i + 5] + 2 * wynik[i + 6] + wynik[i + 7]);
                bw.Write(byteOutput);
                byteOutput = 0;
            }
            bw.Close();
            doneDecoding.Text = "Rozszyfrowano";

        }

        private void Execute3SC_Click(object sender, RoutedEventArgs e)
        {
            string inputFilename = @"C:\elo\test.bin";

            byte[] fileBytes = File.ReadAllBytes(inputFilename);
            string strBin = string.Empty;
            byte btindx = 0;
            string strAllbin = string.Empty;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < fileBytes.Length; i++)
            {
                btindx = fileBytes[i];

                strBin = Convert.ToString(btindx, 2); // Convert from Byte to Bin
                strBin = strBin.PadLeft(8, '0');  // Zero Pad

                sb.Append(strBin);
            }
            strAllbin = sb.ToString();

            int[] source = new int[strAllbin.Length];

            int k = 0;

            foreach (char c in strAllbin)
            {
                source[k++] = Convert.ToInt32(c);
            }

            for (int i = 0; i < source.Length; i++)
            {
                source[i] = source[i] - 48;
            }

            //GOTOWA TABLICA INTOW 






            int liczba_przesuniec = source.Length;
            int[] wyjscie = new int[liczba_przesuniec];
            int suma = 0;
            string seed_str = lsfr_seed2.Text;
            string poly_str = lsfr_polynomial2.Text;


            int[] seed = new int[seed_str.Length];
            int[] polynomial = new int[poly_str.Length];

            for (int i = 0; i < seed_str.Length; i++)
            {
                seed[i] = seed_str[i] - '0';
            }
            for (int i = 0; i < poly_str.Length; i++)
            {
                polynomial[i] = poly_str[i] - '0';
            }

            int[] new_seed = new int[seed.Length];



            for (int i = 0; i < liczba_przesuniec; i++)
            {
                for (int j = 0; j < seed.Length; j++)
                {
                    if (polynomial[j] == 1)
                    {
                        suma = suma + seed[j];
                    }

                }

                for (int j = 0; j < (seed.Length - 1); j++)
                {
                    new_seed[j + 1] = seed[j];
                }
                new_seed[0] = (suma+source[i]) % 2;
                for (int j = 0; j < new_seed.Length; j++)
                {
                    seed[j] = new_seed[j];
                }

                suma = 0;
                wyjscie[i] = seed[0];
            }
            
            

            byte byteOutput = 0;
            BinaryWriter bw = new BinaryWriter(new FileStream(@"C:\elo\testoutput3SC.bin", FileMode.Create));

            for (int i = 0; i < wyjscie.Length; i += 8)
            {
                byteOutput = Convert.ToByte(128 * wyjscie[i] + 64 * wyjscie[i + 1] + 32 * wyjscie[i + 2] + 16 * wyjscie[i + 3] + 8 * wyjscie[i + 4] + 4 * wyjscie[i + 5] + 2 * wyjscie[i + 6] + wyjscie[i + 7]);
                bw.Write(byteOutput);
                byteOutput = 0;


            }
            bw.Close();
            doneCoding3SC.Text = "Zakodowano";

        }

        private void GoBack3SC_Click(object sender, RoutedEventArgs e)
        {
            string inputFilename = @"C:\elo\testoutput3SC.bin";
            byte[] fileBytes = File.ReadAllBytes(inputFilename);
            string strBin = string.Empty;
            byte btindx = 0;
            string strAllbin = string.Empty;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < fileBytes.Length; i++)
            {
                btindx = fileBytes[i];

                strBin = Convert.ToString(btindx, 2); // Convert from Byte to Bin
                strBin = strBin.PadLeft(8, '0');  // Zero Pad

                sb.Append(strBin);
            }
            strAllbin = sb.ToString();

            int[] source = new int[strAllbin.Length];

            int k = 0;

            foreach (char c in strAllbin)
            {
                source[k++] = Convert.ToInt32(c);
            }

            for (int i = 0; i < source.Length; i++)
            {
                source[i] = source[i] - 48;
            }

            //gotowa tablica intow


            int liczba_przesuniec = source.Length;
            int[] wyjscie = new int[liczba_przesuniec];
            int suma = 0;
            string seed_str = lsfr_seed2.Text;
            string poly_str = lsfr_polynomial2.Text;


            int[] seed = new int[seed_str.Length];
            int[] polynomial = new int[poly_str.Length];

            for (int i = 0; i < seed_str.Length; i++)
            {
                seed[i] = seed_str[i] - '0';
            }
            for (int i = 0; i < poly_str.Length; i++)
            {
                polynomial[i] = poly_str[i] - '0';
            }

            int[] new_seed = new int[seed.Length];



            for (int i = 0; i < liczba_przesuniec; i++)
            {
                for (int j = 0; j < seed.Length; j++)
                {
                    if (polynomial[j] == 1)
                    {
                        suma = suma + seed[j];
                    }

                }

                for (int j = 0; j < (seed.Length - 1); j++)
                {
                    new_seed[j + 1] = seed[j];
                }
                new_seed[0] = source[i];
                for (int j = 0; j < new_seed.Length; j++)
                {
                    seed[j] = new_seed[j];
                }
                wyjscie[i] = (suma +source[i])%2;
                suma = 0;
                
            }
            
           

            BinaryWriter bw = new BinaryWriter(new FileStream(@"C:\elo\testDeconverted3SC.bin", FileMode.Create));

            byte byteOutput = 0;


            for (int i = 0; i < wyjscie.Length; i += 8)
            {
                byteOutput = Convert.ToByte(128 * wyjscie[i] + 64 * wyjscie[i + 1] + 32 * wyjscie[i + 2] + 16 * wyjscie[i + 3] + 8 * wyjscie[i + 4] + 4 * wyjscie[i + 5] + 2 * wyjscie[i + 6] + wyjscie[i + 7]);
                bw.Write(byteOutput);
                byteOutput = 0;
            }
            bw.Close();
            doneDecoding3SC.Text = "Rozszyfrowano";

        }
    }

}
