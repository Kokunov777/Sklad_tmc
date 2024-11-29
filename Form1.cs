using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using static Sklad_tmc.Form1;

namespace Sklad_tmc
{
    public partial class Form1 : Form
    {
        // Список складов, который хранится в памяти
        private List<Warehouse> Warehouses = new List<Warehouse>();
        public List<Warehouse> GetWarehouses()
        {
            return Warehouses;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Проверка на пустые поля
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            try
            {
                // Создание нового склада с данными из текстовых полей
                Warehouse newWarehouse = new Warehouse
                {
                    Name = textBox1.Text,              // Название склада
                    Type = textBox2.Text,              // Тип склада
                    Area = Convert.ToDouble(textBox3.Text),  // Площадь склада
                    Address = textBox4.Text             // Адрес склада
                };

                // Добавление нового склада в список
                Warehouses.Add(newWarehouse);

                // Обновляем отображение списка складов
                UpdateWarehouseList();

                // Очистка полей ввода
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат данных. Площадь должна быть числом.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void UpdateWarehouseList()
        {
            listBox1.Items.Clear(); // Очищаем ListBox перед обновлением
            foreach (var warehouse in Warehouses)
            {
                listBox1.Items.Add($"{warehouse.Name} - {warehouse.Type} - {warehouse.Area} m² - {warehouse.Address}");
            }
        }

        // Обновление отображения списка складов в ListBox
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (var warehouse in Warehouses)
            {
                listBox1.Items.Add($"{warehouse.Name} - {warehouse.Type} - {warehouse.Area} m² - {warehouse.Address}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveWarehousesToFile();
            MessageBox.Show("Данные складов сохранены в файл.");

        }
        private void SaveWarehousesToFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Warehouse>));
            string filePath = "warehouses.xml"; // Путь к файлу

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, Warehouses);
            }
        }

        private void LoadWarehousesFromFile()
        {
            string filePath = "warehouses.xml"; // Путь к файлу

            if (File.Exists(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Warehouse>));
                using (StreamReader reader = new StreamReader(filePath))
                {
                    Warehouses = (List<Warehouse>)serializer.Deserialize(reader);
                }
                UpdateWarehouseList(); // Обновляем ListBox после загрузки
            }
        }
    }
}


