using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using EmployeeManagement.Core;
using EmployeeManagement.Model;
using EmployeeManagement.View;

namespace EmployeeManagement.ViewModel
{
    internal class MainVM : ObservableObject
    {
        /* 
        Так как я здесь буду бОльшую часть своего времени, то проведу краткий экскурс как я созда(-л/-м) навигацию.
        Дело в том, что в прошлой своей работе я использовал навигацию через фрейм (то есть, я создавал страницы и в VM отдавал данные о текущей странице приватному Фрейму, 
        который впоследствии отдавал данные фрейму в MainWindow), но я узнал о существовании Source.
        С его помощью навигацию делать куда проще, удобнее, понятнее. Кратко говоря, Source у Frame принимает на себя Uri (эдакий URL в мире WPF),
        Если так судить, то достаточно создать все Uri в конструкторе класса, создать команды для перемещения и сделать привязку Source у фрейма в MainWindow

        Таски для вьюшек: 
        Создать страницу приветствия -> сделано (GreetingsPage)
        Создать страницу редактирования/добавления -> сделано (EditPage)
        Создать страницу для просмотра сотрудников -> сделано (LOE - ListOfEmployees)

        Таски для модели:
        Не знаю какие хар-ки должны быть у работников, но я в модель засунул: 
        имя, фамилию, дату рождения (в последствии хочу реализовать помимо даты рождения еще и количество лет, 
        которое считается автоматически, используя нынешнюю дату), должность. -> сделано

        Таски для вм:
        Низнаю, по ходу разберемся :D

        Комментарии: также, я создал папку с корневыми классами, а именно ObservableObject (реализует класс INotifyPropertyChanged)
                     и RelayCommands, с помощью которого удобно создавать команды для разных элементов управления :)

                     Хочу сделать еще дизайн небольшой, не знаю, получился ли он впоследствии или делал ли я его вообще.
        */
        private static MainVM _instance;
        public static MainVM Instance
        {
            get
            {
                MainVM mainVM = _instance = new MainVM();
                return mainVM;
            }
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        private Uri _currentPage;
        public Uri CurrentPage
        {
            get => _currentPage;
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged(nameof(CurrentPage));
                    if (value == new Uri("View/GreetingsPage.xaml", UriKind.Relative))
                    {
                        Title = "Приветствие";
                    }
                    else if (value == new Uri("View/ListOfEmployees.xaml", UriKind.Relative))
                    {
                        Title = "Лист сотрудников";
                    }
                    else
                    {
                        Title = "Редактирование в списке";
                    }
                }
            }
        }

        public ObservableCollection<Employee> Employees { get; set; }

        public RelayCommand GoGreetingsCommand { get; }
        public RelayCommand GoEditPageCommand { get; }
        public RelayCommand GoLOECommand { get; } //go list of employees command
        public RelayCommand GoAddPageCommand {  get; }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                if (_selectedEmployee != value)
                {
                    _selectedEmployee = value;
                    OnPropertyChanged(nameof(SelectedEmployee));
                }
            }
        }

        private MainVM() 
        {
            Employees = new ObservableCollection<Employee>
            {
                new Employee {Name = "Ilya", Surname = "Kazarin", Birthday = new DateTime(2005, 12, 19).Date, Position = "Маппер"},
                new Employee {Name = "Миша", Surname = "ПРохладный", Birthday = new DateTime(2005, 12, 19).Date, Position = "Игрок"},
                new Employee {Name = "Бляяяяяя", Surname = "Как же я", Birthday = new DateTime(2005, 12, 19), Position = "заебался уже...."}
            };

            var greetingsPage = new Uri("View/GreetingsPage.xaml", UriKind.Relative);
            var loePage = new Uri("View/ListOfEmployees.xaml", UriKind.Relative);
            var editPage = new Uri("View/EditPage.xaml", UriKind.Relative);
            var addPage = new Uri("View/AddPage.xaml", UriKind.Relative);

            GoGreetingsCommand = new RelayCommand(_ => CurrentPage = greetingsPage, null);
            GoEditPageCommand = new RelayCommand(_ => CurrentPage = editPage);
            GoLOECommand = new RelayCommand(_ => CurrentPage = loePage, null);
            GoAddPageCommand = new RelayCommand(_ => CurrentPage = addPage, null);

            GoEditPageCommand.CanExecuteChanged += (s, e) =>
            {
                Console.WriteLine("CanExecuteChanged triggered");
            };

            CurrentPage = greetingsPage;
        }

        public bool NullToBoolConverter(object employee)
        {
            if (employee == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
