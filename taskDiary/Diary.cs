using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace taskDiary
{
    class Diary
    {
        //точка входа
        static void Main()
        {
            Program program = new Program();
            program.main();
        }
    }

    /// <summary>
    /// Класс программы
    /// </summary>
    public class Program
    {
        Business[] businesses = new Business[3];  //массив обьектов будет хранить список дел
        public int main()
        {
            setUp(); //создаем стартовый список

            for (;;)
            {
                try
                {
                    mainMenu();     //выводим список возможных действий
                    int menuItem = int.Parse(Console.ReadLine()); //выбираем одно из них

                    switch (menuItem)
                    {
                        case 1: showMyBusiness(); break; //задача ---> отображение списка дел
                        case 2: addNewBusiness(); break; //задача ---> Добавление в список дел
                        case 3: deliteBusiness(); break; //задача ---> Удаление Заметки
                        case 4: editBusiness(); break;   //задача ---> редактирование Заметки
                        case 5: BubbleSort(); break;     //задача ---> сортировка
                        case 6: WriteInFile(); break;     //задача ---> запись в фаил
                        case 7: ReadInFile(); break;     //задача ---> считывание

                        case 0: return 0;                //выход из программы
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("ошибка " + e);
                }
            }
        }

        // меню возможных действий
        void mainMenu()
        {
            Console.WriteLine("Для вывода всех имеющихся дел введите 1");
            Console.WriteLine("Для добавления дела в список введите 2");
            Console.WriteLine("Для удаления дела из спписка введите 3");
            Console.WriteLine("Для редактирования дела введите 4");
            Console.WriteLine("Для сортировки дел введите 5");
            Console.WriteLine("Для сохранения в фаил введите 6");
            Console.WriteLine("Для чтения из фаила введите 7");
            Console.WriteLine("Для выхода введите 0");
            Console.WriteLine();
        }

        //создаем начальный список
        void setUp()
        {
            businesses[0] = new Business();
            businesses[0].numberBusiness = 1;
            businesses[0].textBusiness = "Написать програму Планировщик";

            businesses[1] = new Business();
            businesses[1].numberBusiness = 2;
            businesses[1].textBusiness = "Функциональность приложения - ведение списка запланированных дел";

            businesses[2] = new Business();
            businesses[2].numberBusiness = 3;
            businesses[2].textBusiness = "Реализовать --> Отображение списка дел";
        }

        //медот выводит информацию о всех заметках
        void showMyBusiness()
        {
            for (int i = 0; i < businesses.Length; i++)
            {
                if(businesses[i] != null) //проверка не удален ли элемент
                {
                    Console.WriteLine("/-----Задача " + businesses[i].numberBusiness + " -----/");
                    Console.WriteLine("" + businesses[i].textBusiness);
                    Console.WriteLine("/----------------------------/");
                }
            }
                
            Console.WriteLine();
        }

        //добавляем заметку
        void addNewBusiness()
        {
            
            try
            {
                Console.WriteLine("Введите номер заметки:");
                int temp = int.Parse(Console.ReadLine());

                Array.Resize(ref businesses, businesses.Length + 1); //увеличиваем длину массина на 1
                businesses[businesses.Length - 1] = new Business(); //работаем с предыдущим элементом

                //нумируем заметки

                businesses[businesses.Length - 1].numberBusiness = temp;

                //текст заметки
                Console.WriteLine("Введите текст заметки:");
                businesses[businesses.Length - 1].textBusiness = Console.ReadLine();

                Console.WriteLine();
            }
            catch
            {
                Console.WriteLine("ошибка");
            }
            
        }

        //удаляем заметку
        void deliteBusiness()
        {
            Console.WriteLine("введите № заметки которую хотите удалить");
            int deliteItem = int.Parse(Console.ReadLine());

            for(int i =0; i< businesses.Length; i++)
            {
                if (deliteItem == businesses[i].numberBusiness)
                {
                    businesses[i] = null;
                    Console.WriteLine("Заметка удалена");
                }
            }
            Console.WriteLine();
        }

        //редактируем заметку
        void editBusiness()
        {
            Console.WriteLine("введите № заметки которую хотите редактировать");
            int deliteItem = int.Parse(Console.ReadLine());

            for (int i = 0; i < businesses.Length; i++)
            {
                if (deliteItem == businesses[i].numberBusiness)
                {
                    Console.WriteLine("ВВедите новый текст");
                    businesses[i].textBusiness = Console.ReadLine();
                    Console.WriteLine("Заметка Отредактирована");
                }
            }
            Console.WriteLine();
        }

        //алгоритм взят готовый
        void BubbleSort()
        {
            int temp;
            for (int i = 0; i < businesses.Length; i++)
            {
                for (int j = i + 1; j < businesses.Length; j++)
                {
                    if (businesses[i].numberBusiness > businesses[j].numberBusiness)
                    {
                        temp = businesses[i].numberBusiness;
                        businesses[i].numberBusiness = businesses[j].numberBusiness;
                        businesses[j].numberBusiness = temp;
                    }
                }
            }
            Console.WriteLine("Сортировка успешна");
            Console.WriteLine();
        }

        //////------блок вывода в фаил--------/////
        // метод вывода данных в фаил по определенному алгоритму
        private void WriteInFile()
        {
            //проверка на создание папка
            CreateIfMissing("data");

            StreamWriter str = new StreamWriter("data//dataBusiness.txt");

            str.WriteLine("Длинна массива"); //нужно для считывания
            str.WriteLine(businesses.Length);

            str.WriteLine("Данные");
            for (int i=0; i<businesses.Length; i++)
            {
                str.WriteLine(businesses[i].numberBusiness);
                str.WriteLine(businesses[i].textBusiness);
            }

            str.Close();
            Console.WriteLine("Запись в фаил успешна --> data//dataBusiness.txt");
            Console.WriteLine();
        }
        
        // метод проверяет существует ли уже папка с введенным именем и если нет - создает ее
        private void CreateIfMissing(string path)
        {
            bool folderExists = Directory.Exists(path);
            if (!folderExists)
                Directory.CreateDirectory(path);
        }
        //////-----/блок вывода в фаил--------/////

        //считываем данные из файла
        private void ReadInFile()
        {
            //Затираем старые записи
            for(int i=0; i<businesses.Length; i++)
            {
                businesses[i] = null;
            }

            //задаем размер новому массиву
            businesses = new Business[Convert.ToInt32(getLines("Длинна массива"))];

            for (int i = 0; i < businesses.Length; i++)
            {
                businesses[i] = new Business();
            }

            getDataBusiness("Данные");

            Console.WriteLine("считывание успешно");
            Console.WriteLine();

        }

        //считываем нужное значение строки в файле
        public string getLines(string neededData)
        {
            using (var reader = new StreamReader("data//dataBusiness.txt"))
            {
                string temp;
                while (true)
                {
                    temp = reader.ReadLine();
                    if (temp.Contains(neededData))
                    {
                        //считываем следующюю строку
                        temp = reader.ReadLine();
                        return temp;
                    }
                }
                reader.Close();
            }
        }

        //в определенном алгоритме записываем массив
        public void getDataBusiness(string neededData)
        {
            using (var reader = new StreamReader("data//dataBusiness.txt"))
            {
                string temp;
                while (true)
                {
                    temp = reader.ReadLine() + "\n";
                    if (temp.Contains(neededData))
                    {
                        for (int i = 0; i < businesses.Length; i++)
                        {
                            businesses[i].numberBusiness = Convert.ToInt32(reader.ReadLine());
                            businesses[i].textBusiness = reader.ReadLine();

                            if (i == businesses.Length - 1)
                            {
                                reader.Close();
                                return;
                            }
                        }
                        

                    }

                }
            }
        }
    }

    /// <summary>
    /// Класс хранит заметки
    /// Business - дело
    /// </summary>
    public class Business
    {
        public int numberBusiness;
        public string textBusiness;
    }
}
