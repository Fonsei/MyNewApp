using MyNewApp.Models.Helpers;
using MyNewApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace MyNewApp.ViewModels
{
    public class StundenBereechnungViewModel : ViewModelBase
    {
        public StundenBereechnungViewModel()
        {
            WochenTage = new ObservableCollection<WochenTag>
            {
                new WochenTag
                {
                    Name = "Mo",
                    Start = new TimeSpan(12,0,0),
                    End = new TimeSpan(19,0,0),
                    Pause = new TimeSpan(0,30,0)
                },
                new WochenTag
                {
                    Name = "Di",
                    Start = new TimeSpan(12,0,0),
                    End = new TimeSpan(18,0,0),
                    Pause = new TimeSpan(0,30,0)
                },
                new WochenTag
                {
                    Name = "Mi",
                    Start = new TimeSpan(12,0,0),
                    End = new TimeSpan(18,0,0),
                    Pause = new TimeSpan(0,30,0)
                },
                new WochenTag
                {
                    Name = "Do",
                    Start = new TimeSpan(12,0,0),
                    End = new TimeSpan(18,0,0),
                    Pause = new TimeSpan(0,30,0)
                },
                new WochenTag
                {
                    Name = "Fr",
                    Start = new TimeSpan(13,0,0),
                    End = new TimeSpan(18,0,0),
                    Pause = new TimeSpan(0,30,0)
                },
                new WochenTag
                {
                    Name = "Sa",
                    Start = new TimeSpan(12,0,0),
                    End = new TimeSpan(17,0,0),
                    Pause = new TimeSpan(0,0,0)
                },
                new WochenTag
                {
                    Name = "So",
                    Start = new TimeSpan(0,0,0),
                    End = new TimeSpan(0,0,0),
                    Pause = new TimeSpan(0,0,0)
                }
            };
        }

        private void Berechnen()
        {
            Mehrstunden = 0;
            WochenStunden = 0;
            foreach (var tag in WochenTage)
            {
                TagBerechnen(tag.Start, tag.End, tag.Pause, tag.Name);
            }
            //BezahlteStunden = WochenStunden;
            //MST25 mehrstunden ab normalzeit
            if (WochenStunden > 40)
            {
                AktWochenstunden = 40;
                var mehrstunden = WochenStunden - 40;
                mehrstunden = mehrstunden * 1.25;
                MST25Stunden += mehrstunden;
                BezahlteStunden = AktWochenstunden + Mehrstunden;
            }

            Mehrstunden += MST25Stunden;
            Mehrstunden += MST50Stunden;
            Mehrstunden += MST70Stunden;


            BezahlteStunden = Mehrstunden + WochenStunden;
        }

        void TagBerechnen(TimeSpan start, TimeSpan end, TimeSpan pause, string tag)
        {
            NormalStunden(start, end, pause);
            //MST50 Samstag nachmittag
            if(tag == "Sa" && end > new TimeSpan(13,0,0))
            {
                var time = end - new TimeSpan(13, 0, 0);
                var mehrstunden = (time.TotalMinutes / 60) * 1.5;
                MST50Stunden += mehrstunden;
            }

            //MST70 ab 18.30 bis 20 uhr
            if (end > new TimeSpan(18,30,0) && end <= new TimeSpan(20,0,0))
            {
                var time = end - new TimeSpan(18, 30, 0);
                var mehrstunden = (time.TotalMinutes/60) * 1.7;
                MST70Stunden += mehrstunden;
            }

            //UST über 40 Stunden aber auch 11. udn 12 stunde

            //UST ab 20 uhr oder Sonntags


        }

        void NormalStunden(TimeSpan start, TimeSpan end, TimeSpan pause)
        {
            if (end < start)
                return;
            var stunden = (end - start);
            stunden = stunden - pause;
            WochenStunden += stunden.TotalMinutes / 60;
        }

        #region Properties     

        public double Mehrstunden { get; set; }
        public double AktWochenstunden { get; set; }

        private ICommand berechnenCommand;

        public ICommand BerechnenCommand
        {
            get
            {
                if (berechnenCommand == null)
                {
                    berechnenCommand = new Command(Berechnen);
                }

                return berechnenCommand;
            }
        }

        private double wochenStunden;

        public double WochenStunden { get => wochenStunden; set { SetProperty(ref wochenStunden, value); OnPropertyChanged(); } }

        private ObservableCollection<WochenTag> wochenTage;

        public ObservableCollection<WochenTag> WochenTage { get => wochenTage; set { SetProperty(ref wochenTage, value); OnPropertyChanged();} }

        private double bezahlteStunden;

        public double BezahlteStunden { get => bezahlteStunden; set { SetProperty(ref bezahlteStunden, value); OnPropertyChanged(); } }

        private double mST25Stunden;

        public double MST25Stunden { get => mST25Stunden; set { SetProperty(ref mST25Stunden, value); OnPropertyChanged(); } }

        private double mST70Stunden;

        public double MST70Stunden { get => mST70Stunden; set { SetProperty(ref mST70Stunden, value); OnPropertyChanged(); } }

        private double mST50Stunden;

        public double MST50Stunden { get => mST50Stunden; set { SetProperty(ref mST50Stunden, value); OnPropertyChanged(); } }


        #endregion
    }
}
