using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sklad_tmc
{
    public partial class Form3 : Form
    {
        private List<Goods> goodsList;

        public Form3(List<Goods> goodsList) // Исправлено название параметра
        {
            InitializeComponent();
            this.goodsList = goodsList;
            FileGoodsComboBox();
        }

        private void FileGoodsComboBox()
        {
            comboBox1.Items.Clear(); // Добавлена очистка ComboBox
            foreach (var good in goodsList)
            {
                comboBox1.Items.Add(good.Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните поле количества и выберите товар.");
                return;
            }

            if (!int.TryParse(textBox1.Text, out int arrivalQuantity))
            {
                MessageBox.Show("Некорректное значение количества.");
                return;
            }

            var selectedGoods = goodsList.FirstOrDefault(g => g.Name == comboBox1.SelectedItem.ToString());

            if (selectedGoods == null)
            {
                MessageBox.Show("Товар не найден.");
                return;
            }

            // Увеличиваем количество на складе
            selectedGoods.Quantity += arrivalQuantity;

            // Обновляем DataGridView
            UpdateArrivalList();

            // Очистка полей
            textBox1.Clear();
        }

        // Обновление отображения списка поступлений
        private void UpdateArrivalList()
        {
            dataGridView1.Rows.Clear();
            foreach (var goods in goodsList)
            {
                dataGridView1.Rows.Add(goods.Name, goods.Quantity);
            }
        }
    }
}

