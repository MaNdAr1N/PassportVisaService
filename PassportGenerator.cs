namespace PassportVisaService
{
    internal class PassportGenerator
    {
        /// <summary>
        /// Функция обработки изображения || Создание паспорта
        /// </summary>
        /// <param name="series">Серия паспорта</param>
        /// <param name="number">Номер паспорта</param>
        /// <param name="firstName">Имя</param>
        /// <param name="lastName">Фамилия</param>
        /// <param name="patronymic">Отчество</param>
        /// <param name="birthday">Дата рождения</param>
        /// <param name="gender">Пол</param>
        /// <param name="city">Город</param>
        /// <param name="photoURL">Путь до изображения</param>
        /// <returns>При удачном обработке и сохранении - true</returns>
        public static bool TryPassGeneration(string series, string number, string firstName, string lastName, string patronymic, string birthday, bool gender, string city, string photoURL)
        {
            try
            {
                var seriesSplit = series.Split(' '); // Создаем массив из серии паспорта (входная маска: 90 00). Делим по пробелу.
                File.Copy("passport.jpg", $"passport_{series} {number}.jpg"); // Создаем копию пустого паспорта
                ResizePhoto(photoURL, $"resized_{series} {number}.jpg", 180, 200); // Изменяем размер фотографии (аватар)
                var avaImage = Image.FromFile($"resized_{series} {number}.jpg"); // Сохраняем измененную фотографию (аватар)
                var passportImage = Image.FromFile($"passport_{series} {number}.jpg"); // Сохраняем в переменную копию паспорта
                var passport = Graphics.FromImage(passportImage); // Отрисовываем паспорт в переменной

                passport.DrawImage(avaImage, 37, 532); // Отрисовываем аватар на фотографии паспорта

                StringFormat stringFormat = new StringFormat(); // Создаем объект и инициализируем его || Для управления форматом текста
                stringFormat.FormatFlags = StringFormatFlags.DirectionVertical; // Задаем вертикальное положение для этого формата
                passport.DrawString($"{seriesSplit[0]}    {seriesSplit[1]}", new Font("Tahoma", 16), new SolidBrush(Color.FromArgb(103, 38, 58)), 575, 546, stringFormat); //Отрисовываем серию паспорта
                passport.DrawString(number, new Font("Tahoma", 16), new SolidBrush(Color.FromArgb(103, 38, 58)), 575, 655, stringFormat); //Отрисовываем номер паспорта

                passport.DrawString(firstName.ToUpper(), new Font("Tahoma", 16, FontStyle.Bold), new SolidBrush(Color.FromArgb(92, 69, 77)), 264, 546); // Отрисовываем Имя
                passport.DrawString(lastName.ToUpper(), new Font("Tahoma", 16, FontStyle.Bold), new SolidBrush(Color.FromArgb(92, 69, 77)), 288, 480); // Отрисовываем Фамилию
                passport.DrawString(patronymic.ToUpper(), new Font("Tahoma", 16, FontStyle.Bold), new SolidBrush(Color.FromArgb(92, 69, 77)), 300, 580); // Отрисовываем Отчество
                passport.DrawString(birthday, new Font("Tahoma", 16), new SolidBrush(Color.FromArgb(92, 69, 77)), 380, 610); // Отрисовываем Дату рождения
                passport.DrawString(gender ? "МУЖ." : "ЖЕН.", new Font("Tahoma", 16), new SolidBrush(Color.FromArgb(92, 69, 77)), 240, 613); // Отрисовываем Пол
                passport.DrawString($"ГОР. {city.ToUpper()}", new Font("Tahoma", 16), new SolidBrush(Color.FromArgb(92, 69, 77)), 275, 645); // Отрисовываем Город

                passportImage.Save($"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}/{series} {number} {lastName} {firstName} {patronymic}.jpg"); // Сохранение фотограции на рабочий стол

                // Очистка ресурсов Windows для System.Drawing.Image
                avaImage.Dispose();
                passportImage.Dispose();
                passport.Dispose();

                // Удаление временных файлов
                File.Delete($"passport_{series} {number}.jpg");
                File.Delete($"{series} {number}.jpg");
                File.Delete($"resized_{series} {number}.jpg");
                return true; // Возврат удачного результата
            }
            catch
            {
                // Удаление временных файлов (если они существуют)
                File.Delete($"passport_{series} {number}.jpg");
                File.Delete($"{series} {number}.jpg");
                File.Delete($"resized_{series} {number}.jpg");
                File.Delete($"passport_new_{series} {number}.jpg");
                return false; // Возврат неудачного результата
            }
        }

        /// <summary>
        /// Изменение размера изображения
        /// </summary>
        /// <param name="pathPhoto">Путь входного изображения</param>
        /// <param name="pathPhotoOut">Путь выходного</param>
        /// <param name="sizeX">Размер по горизонтали</param>
        /// <param name="sizeY">Размер по вертикали</param>
        private static void ResizePhoto(string pathPhoto, string pathPhotoOut, int sizeX, int sizeY)
        {
            var img = Image.FromFile(pathPhoto); // Записываем изображение в переменную
            var bmp = new Bitmap(img, sizeX, sizeY); // Изменяем размер изображения с помощью Bitmap
            bmp.Save(pathPhotoOut); // Сохраняем результат

            // Очистка ресурсов Windows для System.Drawing.Image
            img.Dispose();
            bmp.Dispose();
        }
    }
}
