using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace POSレジ
{
    public partial class Form1 : Form
    {
        TextBox txtCode;
        TextBox txtCash;

        Label lblSubtotal;
        Label lblTax;
        Label lblTotal;
        Label lblChange;

        DataGridView dgvItems;

        int subtotal = 0;

        Dictionary<string, (string Name, int Price)> products =
            new()
            {
                {"1100", ("エアリズムTシャツ",1500)},
                {"1002", ("ジーンズ",3990)},
                {"1003", ("パーカー",2990)},
                {"1004", ("ソックス",390)},
                {"1005", ("ヒートテック",1990)}
            };

        public Form1()
        {
            InitializeComponent();
            CreatePOSScreen();
        }

        private void CreatePOSScreen()
        {
            Text = "SELF CHECKOUT POS SYSTEM";
            Size = new Size(900, 650);
            BackColor = Color.White;
            StartPosition = FormStartPosition.CenterScreen;

            Controls.Clear();

            Label title = new Label();
            title.Text = "SELF CHECKOUT POS SYSTEM";
            title.Font = new Font("Yu Gothic UI", 18, FontStyle.Bold);
            title.ForeColor = Color.Red;
            title.AutoSize = true;
            title.Location = new Point(220, 20);
            Controls.Add(title);

            Label codeLabel = new Label();
            codeLabel.Text = "商品コード";
            codeLabel.Location = new Point(50, 80);
            Controls.Add(codeLabel);

            txtCode = new TextBox();
            txtCode.Location = new Point(140, 75);
            txtCode.Width = 200;
            Controls.Add(txtCode);

            Button btnAdd = new Button();
            btnAdd.Text = "商品追加";
            btnAdd.Location = new Point(360, 73);
            btnAdd.Click += BtnAdd_Click;
            Controls.Add(btnAdd);

            dgvItems = new DataGridView();
            dgvItems.Location = new Point(50, 120);
            dgvItems.Size = new Size(780, 250);

            dgvItems.Columns.Add("Name", "商品名");
            dgvItems.Columns.Add("Qty", "数量");
            dgvItems.Columns.Add("Price", "単価");
            dgvItems.Columns.Add("Amount", "金額");

            dgvItems.AllowUserToAddRows = false;
            dgvItems.ReadOnly = true;
            dgvItems.RowHeadersVisible = false;
            dgvItems.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            Controls.Add(dgvItems);

            lblSubtotal = new Label();
            lblSubtotal.Text = "小計：¥0";
            lblSubtotal.Location = new Point(50, 400);
            lblSubtotal.AutoSize = true;
            Controls.Add(lblSubtotal);

            lblTax = new Label();
            lblTax.Text = "消費税：¥0";
            lblTax.Location = new Point(50, 430);
            lblTax.AutoSize = true;
            Controls.Add(lblTax);

            lblTotal = new Label();
            lblTotal.Text = "合計：¥0";
            lblTotal.Font = new Font("Yu Gothic UI", 12, FontStyle.Bold);
            lblTotal.Location = new Point(50, 470);
            lblTotal.AutoSize = true;
            Controls.Add(lblTotal);

            Label cashLabel = new Label();
            cashLabel.Text = "お預かり";
            cashLabel.Location = new Point(450, 400);
            Controls.Add(cashLabel);

            txtCash = new TextBox();
            txtCash.Location = new Point(530, 395);
            txtCash.Width = 150;
            Controls.Add(txtCash);

            lblChange = new Label();
            lblChange.Text = "お釣り：¥0";
            lblChange.Location = new Point(450, 430);
            lblChange.AutoSize = true;
            Controls.Add(lblChange);

            Button btnPay = new Button();
            btnPay.Text = "会計";
            btnPay.BackColor = Color.Red;
            btnPay.ForeColor = Color.White;
            btnPay.Size = new Size(120, 50);
            btnPay.Location = new Point(450, 500);
            btnPay.Click += BtnPay_Click;
            Controls.Add(btnPay);

            Button btnClear = new Button();
            btnClear.Text = "取消";
            btnClear.Size = new Size(120, 50);
            btnClear.Location = new Point(580, 500);
            btnClear.Click += BtnClear_Click;
            Controls.Add(btnClear);

            Button btnExit = new Button();
            btnExit.Text = "終了";
            btnExit.Size = new Size(120, 50);
            btnExit.Location = new Point(710, 500);
            btnExit.Click += (s, e) => Close();
            Controls.Add(btnExit);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string code = txtCode.Text.Trim();

            MessageBox.Show($"入力されたコード: [{code}]");

            if (!products.ContainsKey(code))
            {
                MessageBox.Show("商品コードが存在しません");
                return;
            }

            var item = products[code];

            dgvItems.Rows.Add(
                item.Name,
                1,
                item.Price,
                item.Price);

            subtotal += item.Price;
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            int tax = (int)(subtotal * 0.1);
            int total = subtotal + tax;

            lblSubtotal.Text = $"小計：¥{subtotal:N0}";
            lblTax.Text = $"消費税：¥{tax:N0}";
            lblTotal.Text = $"合計：¥{total:N0}";
        }

        private void BtnPay_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtCash.Text, out int cash))
            {
                MessageBox.Show("金額を入力してください");
                return;
            }

            int total = subtotal + (int)(subtotal * 0.1);

            if (cash < total)
            {
                MessageBox.Show("お預かり金額が不足しています");
                return;
            }

            lblChange.Text =
                $"お釣り：¥{cash - total:N0}";

            MessageBox.Show("会計完了");
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            dgvItems.Rows.Clear();

            subtotal = 0;

            UpdateTotal();

            txtCash.Clear();

            lblChange.Text = "お釣り：¥0";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}