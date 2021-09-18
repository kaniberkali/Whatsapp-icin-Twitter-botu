using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Threading;
using System.Drawing;

namespace Whatsapp_için_Twitter_botu
{
    class Program
    {
         static void Main(string[] args)
        {
            ChromeDriver Whatsapp;
            ChromeDriver Twitter;
            Console.Title = "Whatsapp için Twitter botu";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Website : kodzamani.weebly.com");
            Console.WriteLine("instagram : @kodzamani.tk");
            Console.WriteLine("------------------------------");
            Console.WriteLine("Whatsapp ve Twitter sürücüleri hazırlanıyor.");
            ChromeOptions option = new ChromeOptions();
            option.AddArgument("headless");
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            Whatsapp = new ChromeDriver(service);
            Twitter = new ChromeDriver(service, option);
            Whatsapp.Navigate().GoToUrl("https://web.whatsapp.com/");
            Console.WriteLine("Whatsapp ve Twitter sürücüleri hazır.");
            for (; ; )
            {
                Thread.Sleep(2000);
                try
                {
                    string kontrol = Whatsapp.FindElement(By.XPath("//html/body/div[1]/div[1]/div[1]/div[3]/div/header/div[1]/div/img")).GetAttribute("src");
                    try
                    {
                        string kontrol2 = Whatsapp.FindElement(By.XPath("//html/body/div[1]/div[1]/div[1]/div[4]/div[1]/header/div[1]/div/img")).GetAttribute("src");
                        Console.WriteLine("Grup veya hesap başarıyla seçildi.");
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Grup veya hesap seçmeniz bekleniyor.");
                    }
                }
                catch
                {
                    Console.WriteLine("Whatsapp hesabınıza giriş yapmanız bekleniyor.");
                }
            }
            Whatsapp.Manage().Window.Position = new Point(-32000, -32000);
            Console.Clear();
            mesajgonder(Whatsapp, "Program başarıyla başlatıldı. Komutları öğrenmek için !komutlar yazın. website : https://kodzamani.weebly.com");
            for(; ; )
            {
                int count = Whatsapp.FindElements(By.XPath("//span[@class='i0jNr selectable-text copyable-text']/span")).Count;
                string mesaj = Whatsapp.FindElements(By.XPath("//span[@class='i0jNr selectable-text copyable-text']/span"))[count-1].Text;
                mesaj = mesaj.ToLower();
                if (mesaj.Substring(0, 1) == "!")
                {
                    if (mesaj == "!komutlar")
                    {
                        mesajgonder(Whatsapp, "!kullaniciadi takip\n" +
                            "!kullaniciadi takipci\n" +
                            "!kullaniciadi isim\n" +
                            "!kullaniciadi twit\n" +
                            "!kullaniciadi twit id\n" +
                            "!kullaniciadi yorum\n" +
                            "!kullaniciadi yorum id\n" +
                            "!kullaniciadi retweet\n" +
                            "!kullaniciadi retweet id\n" +
                            "!kullaniciadi beğeni\n" +
                            "!kullaniciadi beğeni id"
                            );
                    }
                    else if (mesaj.Contains("!kullaniciadi")==false)
                    {
                        try
                        {
                            int vericount = 0;
                            string[] veriler = mesaj.Replace("!","").Split(' ');
                            if (veriler.Length ==3)
                            {
                                vericount = Convert.ToInt32(veriler[2]);
                            }
                            if (Twitter.Url != "https://twitter.com/" + veriler[0])
                            Twitter.Navigate().GoToUrl("https://twitter.com/" + veriler[0]);
                            Thread.Sleep(2000);
                            switch (veriler[1])
                            {
                                case "takip":
                                    string takip = Twitter.FindElements(By.XPath("//html/body/div/div/div/div[2]/main/div/div/div/div[1]/div/div[2]/div/div/div[1]/div/div[4]/div[1]/a/span[1]/span"))[vericount].Text;
                                    mesajgonder(Whatsapp, takip);
                                    break;

                                case "takipci":
                                    string takipci = Twitter.FindElements(By.XPath("//html/body/div/div/div/div[2]/main/div/div/div/div[1]/div/div[2]/div/div/div[1]/div/div[4]/div[2]/a/span[1]/span"))[vericount].Text;
                                    mesajgonder(Whatsapp, takipci);
                                    break;

                                case "isim":
                                    string isim = Twitter.FindElements(By.XPath("//html/body/div/div/div/div[2]/main/div/div/div/div[1]/div/div[2]/div/div/div[1]/div/div[2]/div/div/div[1]/div/span[1]/span"))[vericount].Text;
                                    mesajgonder(Whatsapp, isim);
                                    break;

                                case "twit":
                                    string twit = Twitter.FindElements(By.XPath("//div[@class='css-901oao r-18jsvk2 r-37j5jr r-a023e6 r-16dba41 r-rjixqe r-bcqeeo r-bnwqim r-qvutc0']/span"))[vericount].Text;
                                    mesajgonder(Whatsapp, twit);
                                    break;

                                case "yorum":
                                    string yorum = Twitter.FindElements(By.XPath("//html/body/div/div/div/div[2]/main/div/div/div/div[1]/div/div[2]/div/div/div[2]/section/div/div/div[1]/div/div/article/div/div/div/div[2]/div[2]/div[2]/div[3]/div"))[vericount].GetAttribute("aria-label");
                                    string sonuc = yorum.Split(',')[0];
                                    mesajgonder(Whatsapp, sonuc);
                                    break;

                                case "retweet":
                                    string retweet = Twitter.FindElements(By.XPath("//html/body/div/div/div/div[2]/main/div/div/div/div[1]/div/div[2]/div/div/div[2]/section/div/div/div[1]/div/div/article/div/div/div/div[2]/div[2]/div[2]/div[3]/div"))[vericount].GetAttribute("aria-label");
                                    string sonuc2 = retweet.Split(',')[1];
                                    mesajgonder(Whatsapp, sonuc2);
                                    break;

                                case "beğeni":
                                    string beğeni = Twitter.FindElements(By.XPath("//html/body/div/div/div/div[2]/main/div/div/div/div[1]/div/div[2]/div/div/div[2]/section/div/div/div[1]/div/div/article/div/div/div/div[2]/div[2]/div[2]/div[3]/div"))[vericount].GetAttribute("aria-label");
                                    string sonuc3 = beğeni.Split(',')[2];
                                    mesajgonder(Whatsapp, sonuc3);
                                    break;
                            }
                        }
                        catch
                        {

                        }
                    }
                }
                Thread.Sleep(2000);
            }
        }
         static void mesajgonder(ChromeDriver Whatsapp, string mesaj)
         {
            try
            {
                Whatsapp.FindElement(By.XPath("//html/body/div[1]/div[1]/div[1]/div[4]/div[1]/footer/div[1]/div/div/div[2]/div[1]/div/div[2]")).SendKeys(mesaj);
                Thread.Sleep(200);
                Whatsapp.FindElement(By.XPath("//html/body/div[1]/div[1]/div[1]/div[4]/div[1]/footer/div[1]/div/div/div[2]/div[2]/button")).Click();
                Console.WriteLine("---------------------------------------");
                Console.WriteLine(mesaj);
            }
            catch
            {
                Console.WriteLine("Mesaj gönderilemedi.");
            }
        }
    }
}
