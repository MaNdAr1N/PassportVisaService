namespace PassportVisaService
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize(); // ������������� ������������ ����������
            //Application.Run(new Form1());
            Form2 form2 = new Form2(); // ������� � �������������� �����
            form2.ShowDialog(); // ���������� ����� ��� ������
        }
    }
}