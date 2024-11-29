using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Sklad_tmc.Form1;

namespace Sklad_tmc
{
    public partial class Form2 : Form
    {
        private List<Goods> goodsList = new List<Goods>();
        private List<Warehouse> warehouses;

        public Form2(List<Warehouse> warehouses)
        {
            InitializeComponent();
            this.warehouses = warehouses;
            FillWarehouseComboBox();
        }

        // Заполнение ComboBox складов
        private void FillWarehouseComboBox()
        {
            comboBox1.Items.Clear();
            foreach (var warehouse in warehouses)
            {
                comboBox1.Items.Add(warehouse.Name);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // Проверка на пустые поля
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            // Проверка на корректность ввода количества
            if (!int.TryParse(textBox3.Text, out int quantity))
            {
                MessageBox.Show("Некорректное значение количества.");
                return;
            }


            // Получаем выбранный склад
            string selectedWarehouseName = comboBox1.SelectedItem.ToString();


            // Создаем новый объект ТМЦ
            Goods newGoods = new Goods
            {
                Name = textBox1.Text,
                Type = textBox2.Text,
                Quantity = quantity,
                WarehouseName = selectedWarehouseName // Изменено: храним имя склада
            };
            goodsList.Add(newGoods);

            // Обновляем DataGridView
            UpdateGoodsList();

            // Очистка полей
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        // Обновление отображения списка ТМЦ
        private void UpdateGoodsList()
        {
            dataGridView1.Rows.Clear();
            foreach (var goods in goodsList)
            {
                dataGridView1.Rows.Add(goods.Name, goods.Type, goods.Quantity, goods.WarehouseName);
            }
        }
    }
}
