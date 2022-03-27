using System;

namespace ver1
{
    public interface IDevice
    {
        enum State {on, off};

        void PowerOn(); // uruchamia urządzenie, zmienia stan na `on`
        void PowerOff(); // wyłącza urządzenie, zmienia stan na `off
        State GetState(); // zwraca aktualny stan urządzenia

        int Counter {get;}  // zwraca liczbę charakteryzującą eksploatację urządzenia,
                            // np. liczbę uruchomień, liczbę wydrukow, liczbę skanów, ...
    }

    public abstract class BaseDevice : IDevice
    {
        protected IDevice.State state = IDevice.State.off;
        public IDevice.State GetState() => state;

        public void PowerOff()
        {
            state = IDevice.State.off;
            Console.WriteLine("... Device is off !");
        }

        public void PowerOn()
        {
            state = IDevice.State.on;
            Console.WriteLine("Device is on ...");  
        }

        public int Counter { get; private set; } = 0;
    }

    public interface IPrinter : IDevice
    {
        /// <summary>
        /// Dokument jest drukowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
        /// </summary>
        /// <param name="document">obiekt typu IDocument, różny od `null`</param>
        void Print(in IDocument document);
    }


    //klasa drukarki
    public abstract class Printer : BaseDevice, IPrinter
    {
        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
            {
                Console.WriteLine("Printer is printing ...");
                Counter++;
            }
            else
            {
                Console.WriteLine("Printer is off !");
            }
        }
    }

    public interface IScanner : IDevice
    {
        // dokument jest skanowany, jeśli urządzenie włączone
        // w przeciwnym przypadku nic się dzieje
        void Scan(out IDocument document, IDocument.FormatType formatType);


    }

    //klasa skanera
    public abstract class Scanner : BaseDevice, IScanner
    {
        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            if (state == IDevice.State.on)
            {
                Console.WriteLine("Scanner is scanning ...");
                Counter++;
                document = new ImageDocument("aaa.jpg");
            }
            else
            {
                Console.WriteLine("Scanner is off !");
                document = null;
            }
        }
    }


    // interfejs kserokopiarka
    public interface ICopier : IDevice
    {
        // dokument jest skopiowany, jeśli urządzenie włączone
        // w przeciwnym przypadku nic się dzieje
        void Print(out IDocument document, IDocument.FormatType formatType);
        void Scan(out IDocument document, IDocument.FormatType formatType);
        void ScanAndPrint(out IDocument document, IDocument.FormatType formatType);
        int Counter {get;}
        int PrintCounter {get;}
        int ScanCounter {get;}
    }

    //klasa kserokopiarki
    public abstract class Copier : BaseDevice, ICopier
    {
        public void Print(out IDocument document, IDocument.FormatType formatType)
        {
            if (state == IDevice.State.on)
            {
                Console.WriteLine(DateTime());
                Counter++;
                document = new PDFDocument("aaa.pdf");
            }
            else
            {
                Console.WriteLine("Copier is off !");
                document = null;
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            if (state == IDevice.State.on)
            {
                Console.WriteLine("Copier is scanning ...");
                Counter++;
                document = new TextDocument("aaa.txt");
            }
            else
            {
                Console.WriteLine("Copier is off !");
                document = null;
            }
        }

        public void ScanAndPrint(out IDocument document, IDocument.FormatType formatType)
        {
            if (state == IDevice.State.on)
            {
                Console.WriteLine("Copier is scanning and printing ...");
                Counter++;
                document = new ImageDocument("aaa.jpg");
            }
            else
            {
                Console.WriteLine("Copier is off !");
                document = null;
            }
        }

        public int Counter { get; private set; } = 0;
        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;
    }

}
