namespace PassportVisaService
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent(); // Инициализация компонентов формы
        }

        public string URL; // Использование переменной для хранения пути до готового файла

        // Получение ссылки из другой формы
        public void SetURL(object sender, string e)
        {
            pictureBox1.ImageLocation = e; // Задаем путь до изображения
            URL = e; // Запоминаем его для дальнейшего использования
        } 

        // Обработка события нажатие на кнопку "Редактировать"
        private void button1_Click(object sender, EventArgs e)
        {
            File.Delete(URL); // Удаляем изображение
            this.Close(); // Закрываем диалог
        }
    }
}
