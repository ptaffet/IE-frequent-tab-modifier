using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlHistoryLibrary;
using System.Security.Cryptography;
using Microsoft.Win32;

namespace HashReverser
{
    class Program
    {
            static StringBuilder sb = new StringBuilder();
        static void Main(string[] args)
        {
            UrlHistoryWrapperClass urlHistory = new UrlHistoryWrapperClass();
            UrlHistoryWrapperClass.STATURLEnumerator enumerator  = urlHistory.GetEnumerator();
            HashSet<string> guesses = new HashSet<string>();
            while (enumerator.MoveNext())
            {
                string url = enumerator.Current.pwcsUrl;
                guesses.Add(url);
                for (int i = 0; i < url.Length; i++)
                {
                    if (url[i] == '/' || url[i] == '?')
                    {
                        guesses.Add(url.Substring(0, i));
                        guesses.Add(url.Substring(0, i+1));
                    }
                }
                Console.Write("Found " + guesses.Count + " potential URLs.\r");
            }
            Console.WriteLine();
            Console.WriteLine("Done collecting guesses");
            Console.WriteLine();
            SHA1Cng sha = new SHA1Cng();
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\TabbedBrowsing\NewTabPage\Exclude");
            
            Console.WriteLine("Matches found:");
            foreach (var item in guesses)
            {
                string hash = Hash(sha, item);
                if (key.GetValue(hash) != null)
                {
                    Console.WriteLine(item + "\r\n\t\t" + hash);
                    Console.WriteLine();
                }
            }
            key.Close();
            Console.WriteLine(@"Delete the keys from HKCU\Software\Microsoft\Internet Explorer\TabbedBrowsing\NewTabPage\Exclude that correspond to the URLs you want to restore.");
            Console.WriteLine();
            Console.WriteLine("Done. Press enter to exit.");
            Console.ReadLine();
        }

        private static string Hash(SHA1Cng sha, string item)
        {
            byte[] unicode = new byte[item.Length*2+2];
            UnicodeEncoding.Unicode.GetBytes(item,0, item.Length, unicode, 0);
            byte[] hash = sha.ComputeHash(unicode);
            int sum = 0;
            for (int i = 0; i < hash.Length; i++)
            {
                sum += hash[i];
                sb.AppendFormat("{0:X2}", hash[i]);
            }
            sb.AppendFormat("{0:X2}", (sum % 256));
            string toReturn = sb.ToString();
            sb.Clear();
            return toReturn;
        }
    }
}
