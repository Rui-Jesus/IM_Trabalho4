using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using mmisharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using WindowsInput;
using WindowsInput.Native;

namespace AppGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MmiCommunication mmiC;
        private MmiCommunication mmi_speechMod;
        private LifeCycleEvents lce_speechMod;
        private IWebDriver driver;
        private InputSimulator sim;
        private List<string> tabs;

        private Int32 minWordLen;
        private Int32 minWordCount; 
        public MainWindow()
        {

            InitializeComponent();
            double size = this.Width;
            this.Left = Screen.PrimaryScreen.WorkingArea.Width-size;
            this.Height= Screen.PrimaryScreen.WorkingArea.Height;
            this.Top = 0;
            minWordCount = 3;
            minWordLen = 4;

            driver = new ChromeDriver(".");
            driver.Manage().Window.Position = new System.Drawing.Point(0, 0);
            int driverHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int driverWidth = Screen.PrimaryScreen.WorkingArea.Width - (int)size;
            driver.Manage().Window.Size = new System.Drawing.Size(driverWidth, driverHeight);


            sim = new InputSimulator();
            tabs = driver.WindowHandles.ToList();

            mmiC = new MmiCommunication("localhost", 8000, "User1", "GUI");
            mmiC.Message += MmiC_Message;
            mmiC.Start();

            //Initialize MMI to send messages to speech mod

            lce_speechMod = new LifeCycleEvents("ASR", "FUSION", "speech-2", "acoustic", "command"); // LifeCycleEvents(string source, string target, string id, string medium, string mode)
            mmi_speechMod = new MmiCommunication("localhost", 8000, "User2", "ASR"); // MmiCommunication(string IMhost, int portIM, string UserOD, string thisModalityName)
            mmi_speechMod.Send(lce_speechMod.NewContextRequest());


            //StartUp Gifs of the Helper
            printPossibleCommands("\u2022 Pode pesquisar por termos que pretender (os iniciais são 'futebol','notícias e '')\n\u2022 Pode abrir novas tabs\n");
            img1.Play();
            img2.Play();
            img3.Play();
            img4.Play();


        }
        public void printPossibleCommands(String text)
        {
            command.Text = text;
        }

        public void showGifInfo(List<string> lista)
        {
            int diff = 4 - lista.Count;
            for (int i = 0; i < diff;i++) { lista.Add(" "); }
            
            img1.Source = new Uri("Resources/" + lista[0] + ".gif", UriKind.Relative);
            legend1.Text = lista[0];
            img2.Source = new Uri("Resources/" + lista[1] + ".gif", UriKind.Relative);
            legend2.Text = lista[1];
            img3.Source = new Uri("Resources/" + lista[2] + ".gif", UriKind.Relative);
            legend3.Text = lista[2];
            img4.Source = new Uri("Resources/" + lista[3] + ".gif", UriKind.Relative);
            legend4.Text = lista[3];
        }



        private void MmiC_Message(object sender, MmiEventArgs e)
        {
            Console.WriteLine(e.Message);
            var doc = XDocument.Parse(e.Message);
            var com = doc.Descendants("command").FirstOrDefault().Value;
            dynamic json = JsonConvert.DeserializeObject(com);

            List<String> words = ((JArray)json["recognized"]).ToObject<List<String>>();

            var rec0 = words[0].ToString();
            var rec1 = "";
            int cont = 0;


            //We always receive 3 arguments -> commandID - commandName - confidence
            string commandID = json["recognized"][0];
            var commandName = json["recognized"][1];
            var confidence = json["recognized"][2];


            foreach (string word in words)
            {
                cont++;
            }
            if (cont == 1)
            {
                rec1 = "";
            }
            else { rec1 = words[1].ToString(); }

            string searchText = "";


            foreach (string word in words)
            {
                if (word != "search")
                {
                    searchText += word + " ";
                }
            }

            List<String> numbers = new List<String>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };


            switch (commandID)
            {
                case "0": //Close tab
                    tabs = driver.WindowHandles.ToList();
                    if (tabs.Count() > 1)
                    {
                        driver.Close();
                        driver.SwitchTo().Window(driver.WindowHandles.First());
                    }
                    break;

                case "1": //Historic
                    sim.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                    sim.Keyboard.KeyPress(VirtualKeyCode.VK_H);
                    sim.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    SendTtsMessage("Aqui está o teu histórico de navegação");
                    break;
                    
                case "2": //Maximize
                    driver.Manage().Window.Maximize();
                    break;

                case "3": //Minimize
                    driver.Manage().Window.Minimize();
                    break;

                case "4": //Open Tab
                    sim.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                    sim.Keyboard.KeyPress(VirtualKeyCode.VK_T);
                    sim.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    break;
                    
                case "5": //Scroll down
                    sim.Keyboard.KeyPress(VirtualKeyCode.NEXT);
                    break;

                case "6": //Scroll up
                    sim.Keyboard.KeyPress(VirtualKeyCode.PRIOR);
                    break;
                    
                case "7": //Zoom in
                    sim.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                    sim.Keyboard.KeyPress(VirtualKeyCode.OEM_PLUS);
                    sim.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                    
                    break;
                case "8": //Zoom out
                    sim.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                    sim.Keyboard.KeyPress(VirtualKeyCode.OEM_MINUS);
                    sim.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                    
                    break;
                case "9": //Próxima tab
                    sim.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                    sim.Keyboard.KeyPress(VirtualKeyCode.TAB);
                    sim.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                    //Console.WriteLine(tabs.ToString());
                    tabs = driver.WindowHandles.ToList();
                    int index2 = tabs.IndexOf(driver.CurrentWindowHandle);
                    if (index2 == tabs.Count() - 1)
                    {
                        driver.SwitchTo().Window(driver.WindowHandles.First());
                    }
                    else
                    {
                        driver.SwitchTo().Window(tabs[index2 + 1]);
                    }
                    break;

                case "10": //tab anterior
                    sim.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                    sim.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
                    sim.Keyboard.KeyPress(VirtualKeyCode.TAB);
                    sim.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                    sim.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
                    tabs = driver.WindowHandles.ToList();
                    int index1 = tabs.IndexOf(driver.CurrentWindowHandle);
                    if (index1 == 0)
                    {
                        driver.SwitchTo().Window(driver.WindowHandles.Last());
                    }
                    else
                    {
                        driver.SwitchTo().Window(tabs[index1 - 1]);
                    }
                    break;
                case "11": //favoritos
                    switch (rec1)
                    {
                        default:
                            sim.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                            sim.Keyboard.KeyPress(VirtualKeyCode.VK_D);
                            sim.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                            System.Threading.Thread.Sleep(100);
                            sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                            SendTtsMessage("Adicionado com sucesso aos teus favoritos");
                            break;
                    }
                    break;
                case "12": //ir para a página dos favoritos
                
                    sim.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                    sim.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
                    sim.Keyboard.KeyPress(VirtualKeyCode.VK_O);
                    sim.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                    sim.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
                    SendTtsMessage("Aqui estão os teus favoritos");
                    break;
                case "search": //search
                    sim.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                    sim.Keyboard.KeyPress(VirtualKeyCode.VK_E);
                    sim.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                    Console.WriteLine(searchText);
                    sim.Keyboard.Sleep(200);
                    sim.Keyboard.TextEntry(searchText);
                    System.Threading.Thread.Sleep(200);
                    sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);

                    SendTtsMessage("Estes são os teus resultados");
                    break;
                default:
                    if (driver.Url.Contains("https://www.google.com/search?"))
                    {
                        if (numbers.Contains(rec1))
                        {

                            int linkNumber = Int32.Parse(rec1);

                            ICollection<IWebElement> webElements = driver.FindElements(By.TagName("a"));
                            List<IWebElement> final = new List<IWebElement>();
                            foreach (IWebElement we in webElements)
                            {
                                ICollection<IWebElement> results = we.FindElements(By.TagName("h3"));
                                if (results.Count != 0)
                                {
                                    Console.WriteLine(results.First().Text.ToString() + "\t" + we.GetAttribute("href"));
                                    final.Add(we);
                                }
                            }


                            if (linkNumber < final.Count())
                            {
                                IWebElement element = final[linkNumber - 1];
                                element.Click();
                                string text = "";

                                ICollection<IWebElement> textElements = driver.FindElements(By.TagName("p"));
                                textElements.Concat(driver.FindElements(By.TagName("h1")));
                                textElements.Concat(driver.FindElements(By.TagName("h2")));
                                textElements.Concat(driver.FindElements(By.TagName("h3")));
                                foreach (IWebElement elem in textElements) { text += elem.Text.ToString() + " "; }
                                string[] palavras = text.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                                List<string> pal = palavras.ToList();

                                var counts = new Dictionary<string, int>();

                                foreach (string value in pal)
                                {
                                    if (counts.ContainsKey(value))
                                        counts[value] = counts[value] + 1;
                                    else
                                        counts.Add(value, 1);
                                }
                                List<String> keywords = new List<string>();
                                foreach (string key in counts.Keys)
                                {
                                    if (counts[key] >= minWordCount && key.Length > minWordLen)
                                    {
                                        keywords.Add(key);
                                    }
                                }
                                foreach (string s in keywords)
                                {
                                    Console.WriteLine(s + " ");
                                }

                                //Update speech mod with new words to add to the grammar
                                SendNewWords(keywords);
                                break;
                            }
                            else
                            {
                                int links = final.Count() - 1;
                                SendTtsMessage("Apenas detetei " + links.ToString() + " links que possas abrir");
                            }

                            break;
                        }
                        break;
                    }
                    break;
            }

            /*
             
                -- visit favs
                case "favourites":
                sim.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                sim.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
                sim.Keyboard.KeyPress(VirtualKeyCode.VK_O);
                sim.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                sim.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
                break;
             */


        }

        
        //pesquisa
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            printPossibleCommands("\u2022 Pode fazer scroll up/down\n" +
                "\u2022 Pode aproximar ou afastar página através de zoom in/out\n" +
                "\u2022 Pode consultar o seu histórico\n" +
                "\u2022 Pode escolher qual link abrir, dizendo a sua posição (Ex: Abrir o 2º link)");
            showGifInfo(new List<string> { "Scroll Up", "Scroll Down", "Ver Histórico","Abrir Tab"});         
        }
        //nova tab
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            printPossibleCommands("\u2022 Pode pesquisar por termos que pretender\n" +
                "\u2022 Pode consultar o seu histórico" +
                "\u2022 Abrir/Fechar uma tab");
            showGifInfo(new List<string> {"Ver Histórico", "Fechar Tab" });
        }
        //historico
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            printPossibleCommands("\u2022 Abrir/Fechar uma Tab\n" +
                "\u2022 Fazer zoom in/out\n" +
                "\u2022 Fazer scroll up/down");
            showGifInfo(new List<string> { "Abrir Tab","Fechar Tab" });
        }

        private void img1_MediaEnded(object sender, RoutedEventArgs e)
        {
            img1.Position = TimeSpan.FromMilliseconds(1);
            img1.Play();
        }

        private void img2_MediaEnded(object sender, RoutedEventArgs e)
        {
            img2.Position = TimeSpan.FromMilliseconds(1);
            img2.Play();
        }

        private void img3_MediaEnded(object sender, RoutedEventArgs e)
        {
            img3.Position = TimeSpan.FromMilliseconds(1);
            img3.Play();
        }

        private void img4_MediaEnded(object sender, RoutedEventArgs e)
        {
            img4.Position = TimeSpan.FromMilliseconds(1);
            img4.Play();
        }

        private void SendTtsMessage(string messageToSpeak)
        {
            string json = "{ \"action\" : \"speak\" , \"text_to_speak\" : \"" + messageToSpeak + "\"}";
            var exNot = lce_speechMod.ExtensionNotification("", "", 0, json);
            mmi_speechMod.Send(exNot);
        }

        private void SendNewWords(List<string> words)
        {
            string json = "{ \"action\": \"newWords\", \"newWords\" : [";
            foreach (string word in words)
            {

                json += "\"" + word + "\", ";

            }
            json = json.Substring(0, json.Length - 2);
            json += "] }";
            var exNot = lce_speechMod.ExtensionNotification("", "", 0, json);
            mmi_speechMod.Send(exNot);
        }
    }
}