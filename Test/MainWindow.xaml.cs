using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using RelayCommand;
using Test.Annotations;

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow:INotifyPropertyChanged
    {
        private bool _allowCommand;

        public MainWindow()
        {
            CmdNoArgNoCheck = new RelayCommand.RelayCommand(() => Trace.WriteLine("no arg"));
            CmdArgCheck = new RelayCommand.RelayCommand(arg => Trace.WriteLine($"{arg}"), arg => AllowCommand);
            CmdArgNoCheck = new RelayCommand.RelayCommand(arg => Trace.WriteLine($"{arg}"));
            CmdGenNoArgNoCheck = new RelayCommand<string>(() => Trace.WriteLine("no arg"));
            CmdGenArgNoCheck = new RelayCommand<string>(arg => Trace.WriteLine(arg));
            CmdGenArgCheck = new RelayCommand<string>(arg => Trace.WriteLine(arg), arg => AllowCommand);

            InitializeComponent();
        }
        public RelayCommand.RelayCommand CmdNoArgNoCheck { get; set; }
        public RelayCommand.RelayCommand CmdArgNoCheck { get; set; }
        public RelayCommand.RelayCommand CmdArgCheck { get; set; }
        public RelayCommand<string> CmdGenNoArgNoCheck { get; set; }
        public RelayCommand<string> CmdGenArgNoCheck { get; set; }
        public RelayCommand<string> CmdGenArgCheck { get; set; }

        public bool AllowCommand
        {
            get => _allowCommand;
            set
            {
                _allowCommand = value;
                //OnPropertyChanged();
                //CmdGenArgCheck?.RaiseCanExecuteChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
