using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlatform.Interfaces
{
	class Program
	{
		private static void BuildUI(IUIFactory factory) //... type of platform
		{
            /*
				Call your method for platform here
			*/
            IGrid grid = factory.CreateGrid();

            IButton button1 = factory.CreateButton();
            button1.Content = "BigPurpleButton";
            IButton button2 = factory.CreateButton();
            button2.Content = "SmallButton";
            IButton button3 = factory.CreateButton();
            button3.Content = "Baton";

            grid.AddButton(button1);
            grid.AddButton(button2);
            grid.AddButton(button3);


            ITextBox textBox1 = factory.CreateTextBox();
            textBox1.Content = "";
            ITextBox textBox2 = factory.CreateTextBox();
            textBox2.Content = "EmptyTextBox";
            ITextBox textBox3 = factory.CreateTextBox();
            textBox3.Content = "xoBtxeT";

            grid.AddTextBox(textBox1);
            grid.AddTextBox(textBox2);
            grid.AddTextBox(textBox3);

            var buttons = grid.GetButtons();
            foreach(var b in buttons)
            {
                b.ButtonPressed();
                b.DrawContent();
            }

            var textBoxes = grid.GetTextBoxes();
            foreach (var t in textBoxes)
                t.DrawContent();

            
        }

        static public IUIFactory GetFactory(string platform)
        {
            switch (platform)
            {
                case "iOS":
                    return new iOSFactory();
                case "Windows":
                    return new WindowsFactory();
                case "Android":
                    return new AndroidFactory();
                default:
                    throw new Exception();
            }
        }

        static void Main(string[] args)
		{
            IUIFactory factory;
			Console.WriteLine("<---------------------iOS--------------------->");
            factory = GetFactory("iOS");
            BuildUI(factory);


            Console.WriteLine("<---------------------Windows--------------------->");
            factory = GetFactory("Windows");
            BuildUI(factory);

			Console.WriteLine("<---------------------Android--------------------->");
            factory = GetFactory("Android");
            BuildUI(factory);
			
		}
	}

    //iOS implementation
    public class iOSButton : IButton
    {
        private string content;

        public iOSButton()
        {
            Console.WriteLine("iOS Button created");
        }

        public string Content
        {
            set { content = value; }
        }

        public void DrawContent()
        {
            Console.WriteLine($"{content}");
        }

        public void ButtonPressed()
        {
            Console.WriteLine($"IOS Button pressed, content - {content}");
        }
    }

    public class iOSTextBox: ITextBox
    {
        private string content;

        public iOSTextBox()
        {
            Console.WriteLine("iOS TextBox created");
        }

        public string Content
        {
            set { content = value; }
        }

        public void DrawContent()
        {
            Console.WriteLine($"{content}");
        }
    }

    public class iOSGrid: IGrid
    {
        private List<IButton> ButtonList = new List<IButton>();
        private List<ITextBox> TextBoxList = new List<ITextBox>();

        public iOSGrid()
        {
            Console.WriteLine("iOS Grid created");
        }

        public void AddButton(IButton button)
        {
            ButtonList.Add(button);
        }

        public void AddTextBox(ITextBox textBox)
        {
            TextBoxList.Add(textBox);
        }

        public IEnumerable<IButton> GetButtons()
        {
            for (int i = 0; i < ButtonList.Count; i++)
                yield return ButtonList[i];
        }

        public IEnumerable<ITextBox> GetTextBoxes()
        {
            for (int i = 0; i < TextBoxList.Count; i++)
                yield return TextBoxList[i];
        }
    }

    //Windows implementation
    public class WindowsButton: IButton
    {
        private string content;

        public WindowsButton()
        {
            Console.WriteLine("Windows Button created");
        }

        public string Content
        {
            set { content = value.ToUpper(); }
        }

        public void DrawContent()
        {
            Console.WriteLine($"{content}");
        }

        public void ButtonPressed()
        {
            Console.WriteLine($"Windows button pressed");
        }
    }

    public class WindowsTextBox: ITextBox
    {
        private string content;

        public WindowsTextBox()
        {
            Console.WriteLine("Windows TextBox created");
        }

        public string Content
        {
            set
            {
                StringBuilder s = new StringBuilder();
                for (int i = value.Length / 2; i < value.Length; i++)
                    s.Append(value[i]);
                s.Append(" by .Net Core");
                content = s.ToString();
            }
        }

        public void DrawContent()
        {
            Console.WriteLine($"{content}");
        }
    }

    public class WindowsGrid: IGrid
    {
        private List<IButton> ButtonsList = new List<IButton>();
        private List<ITextBox> TextBoxList = new List<ITextBox>();

        public WindowsGrid()
        {
            Console.WriteLine("Windows Grid created");
        }

        public void AddButton(IButton button)
        {
            ButtonsList.Add(button);
        }

        public void AddTextBox(ITextBox textBox)
        {
            TextBoxList.Add(textBox);
        }

        public IEnumerable<IButton> GetButtons()
        {
            for (int i = ButtonsList.Count - 1; i >= 0; i--)
                yield return ButtonsList[i];
        }

        public IEnumerable<ITextBox> GetTextBoxes()
        {
            if (TextBoxList.Count == 1)
                yield return TextBoxList[0];
            for (int i = TextBoxList.Count - 1; i >= 1; i--)
            {
                if (i == TextBoxList.Count - 1)
                    yield return TextBoxList[0];
                yield return TextBoxList[i];
            }
        }
    }

    //Android implementation
    public class AndroidButton: IButton
    {
        private string content;

        public AndroidButton()
        {
            Console.WriteLine("Android Button created");
        }

        public string Content
        {
            set
            {
                if (value.Length < 9)
                    content = value;
                else
                    content = value.Substring(0, 8);
            }
        }

        public void DrawContent()
        {
            Console.WriteLine($"{content}");
        }

        public void ButtonPressed()
        {
            Console.WriteLine($"Sweet {content}!");
        }
    }

    public class AndroidTextBox: ITextBox
    {
        private string content;

        public AndroidTextBox()
        {
            Console.WriteLine("Android TextBox created");
        }

        public string Content
        {
            set
            {
                StringBuilder tmp = new StringBuilder();
                for (int i = value.Length - 1; i >= 0; i--)  
                    tmp.Append(value[i]);
                content = tmp.ToString();
            }
        }

        public void DrawContent()
        {
            Console.WriteLine($"{content}");
        }
    }

    public class AndroidGrid: IGrid
    {
        private List<IButton> ButtonList = new List<IButton>();
        private List<ITextBox> TextBoxList = new List<ITextBox>();

        public AndroidGrid()
        {
            Console.WriteLine("Android Grid created");
        }

        public void AddButton(IButton button)
        {
            ButtonList.Add(button);
        }

        public void AddTextBox(ITextBox textBox)
        {
            TextBoxList.Add(textBox);
        }

        public IEnumerable<IButton> GetButtons()
        {
            for (int i = 0; i < ButtonList.Count; i++)
                yield return ButtonList[i];
        }

        public IEnumerable<ITextBox> GetTextBoxes()
        {
            //for (int i = 0; i < TextBoxList.Count; i++)
            //    yield return TextBoxList[i];
            return new List<ITextBox>();
        }
    }

    //Abstract Factory
    public interface IUIFactory
    {
        IButton CreateButton();
        ITextBox CreateTextBox();
        IGrid CreateGrid();
    }

    public class iOSFactory: IUIFactory
    {
        public IButton CreateButton() { return new iOSButton(); }
        public ITextBox CreateTextBox() { return new iOSTextBox(); }
        public IGrid CreateGrid() { return new iOSGrid(); }
    }

    public class WindowsFactory : IUIFactory
    {
        public IButton CreateButton() { return new WindowsButton(); }
        public ITextBox CreateTextBox() { return new WindowsTextBox(); }
        public IGrid CreateGrid() { return new WindowsGrid(); }
    }

    public class AndroidFactory : IUIFactory
    {
        public IButton CreateButton() { return new AndroidButton(); }
        public ITextBox CreateTextBox() { return new AndroidTextBox(); }
        public IGrid CreateGrid() { return new AndroidGrid(); }
    }
}

