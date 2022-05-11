using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassportVisaService
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent(); // Инициализация компонентов формы
        }

        private OpenFileDialog ofd; // Создание объекта диалога для выбора файла

        // При загрузке формы
        private void Form2_Load(object sender, EventArgs e)
        {
            ofd = new OpenFileDialog(); // Инициализация объекта диалога для выбора файла
        }

        // Обработчик нажатия на кнопку
        private void button1_Click(object sender, EventArgs e)
        {
            bool isNull = false; // Создание переменной || Для проверки на незаполненное поле
            bool gender = true; // Создание переменной || Для выбора пола

            // Проверки на пустое поле
            if (series.Text == "   ")
            {
                series.BackColor = Color.Red; // Выделение красным
                isNull = true; // Изменение переменной
            }

            if (number.Text == "")
            {
                number.BackColor = Color.Red;
                isNull = true;
            }

            if (firstName.Text == "")
            {
                firstName.BackColor = Color.Red;
                isNull = true;
            }

            if (lastName.Text == "")
            {
                lastName.BackColor = Color.Red;
                isNull = true;
            }

            if (patronymic.Text == "")
            {
                patronymic.BackColor = Color.Red;
                isNull = true;
            }

            if (birthday.Text == "  .  .")
            {
                birthday.BackColor = Color.Red;
                isNull = true;
            }
            
            // Выбор пола
            if (radioButton1.Checked)
                gender = true;
            else if (radioButton2.Checked)
                gender = false;
            else
                isNull = true;

            if (city.Text == "")
            {
                city.BackColor = Color.Red;
                isNull = true;
            }

            if (photoURL.Text == "")
            {
                photoURL.BackColor = Color.Red;
                isNull = true;
            }

            //Если хоть одно поле не заполнено
            if (isNull)
                return;
            else
            {
                // Запуск функции обработки паспорта
                if(PassportGenerator.TryPassGeneration(series.Text, number.Text, firstName.Text, lastName.Text, patronymic.Text, birthday.Text, gender, city.Text, photoURL.Text))
                {
                    // При удачном создании паспорта делаем форму вывода
                    Form1 form1 = new Form1(); // Создание и инициализация формы
                    form1.Owner = this; // Назначение владельца формы
                    form1.SetURL(this, $"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}/" +
                        $"{series.Text} {number.Text} {lastName.Text} {firstName.Text} {patronymic.Text}.jpg"); // Передача ссылки на изображение в форму
                    form1.ShowDialog(); // Отображение формы как диалоговое окно
                    
                }
            }        
        }

        // Обработчик события нажатия на поле
        private void ClickOnText(object sender, EventArgs e)
        {
            // Сброс цвета до изначального
            series.BackColor = Color.White;
            number.BackColor = Color.White;
            firstName.BackColor = Color.White;
            lastName.BackColor = Color.White;
            patronymic.BackColor = Color.White;
            birthday.BackColor = Color.White;
            city.BackColor = Color.White;
            photoURL.BackColor= Color.White;
        }

        // Обработка события нажатия на кнопку "Обзор"
        private void photoURL_button_Click(object sender, EventArgs e)
        {
            // Если диалог завершен нажатием на кнопку "ОК"
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.photoURL.Text = ofd.FileName; // Ввод в поле пути выбранного файла
            }
        }
    }
}
