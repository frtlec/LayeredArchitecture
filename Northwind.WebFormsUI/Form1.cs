using Northwind.Business.Abstract;
using Northwind.Business.Concrete;
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.Entities.Concrete;
using System;
using System.Windows.Forms;

namespace Northwind.WebFormsUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _productService= new ProductManager(new EfProductDal());
            _categoryService = new CategoryManager(new EfCategoryDal());
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private IProductService _productService;
        private ICategoryService _categoryService;

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadCategories();
        }

        private void LoadCategories()
        {
            cbxCategory.DataSource = _categoryService.GetAll();
            cbxCategory.DisplayMember = "CategoryName";
            cbxCategory.ValueMember = "CategoryId";


            cbxCategoryId.DataSource = _categoryService.GetAll();
            cbxCategoryId.DisplayMember = "CategoryName";
            cbxCategoryId.ValueMember = "CategoryId";


            cbxUpdateCategoryId.DataSource = _categoryService.GetAll();
            cbxUpdateCategoryId.DisplayMember = "CategoryName";
            cbxUpdateCategoryId.ValueMember = "CategoryId";

        }

        private void LoadProducts()
        {
            dgwProduct.DataSource = _productService.GetAll();
        }

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgwProduct.DataSource = _productService.GetProductsByCategory(Convert.ToInt32(cbxCategory.SelectedValue));

            }
            catch
            {
            }
                   }

        private void tbxProductName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(tbxProductName.Text))
                {
                    dgwProduct.DataSource = _productService.GetProductsByProductName(tbxProductName.Text);
                }
                else
                {
                    LoadProducts();
                }
                
            }
            catch 
            {
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _productService.Add(new Product
            {
               CategoryId=Convert.ToInt32(cbxCategoryId.SelectedValue),
               ProductName=tbxProductName2.Text,
               QuantityPerUnit=tbxQuantityPerUnit.Text,
               UnitPrice=Convert.ToDecimal(tbxUnitePrice.Text),
               UnitsInStock=Convert.ToInt16(tbxStock.Text)
            });
            MessageBox.Show("ürün kaydedildi.");
            LoadProducts();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _productService.Update(new Product {
                ProductId=Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value),
                ProductName=tbxUpdateProductName.Text,
                CategoryId=Convert.ToInt32(cbxUpdateCategoryId.SelectedValue),
                UnitsInStock=Convert.ToInt16(tbxUpdateStock.Text),
                QuantityPerUnit=tbxQuantityPerUniteUpdate.Text,
                UnitPrice=Convert.ToDecimal(tbxUpdateUnitePrice.Text)
            
            });
            MessageBox.Show("ürün Güncellendi.");
            LoadProducts();
        }

        private void dgwProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgwProduct.CurrentRow;
            tbxUpdateProductName.Text = row.Cells[1].Value.ToString();
            cbxUpdateCategoryId.SelectedValue = row.Cells[2].Value;
            tbxUpdateUnitePrice.Text = row.Cells[3].Value.ToString();
            tbxQuantityPerUniteUpdate.Text = row.Cells[4].Value.ToString();
            tbxUpdateStock.Text = row.Cells[5].Value.ToString();

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgwProduct.CurrentRow!=null)
            {
                try
                {
                    _productService.Delete(new Product
                    {
                        ProductId = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value)

                    });
                    MessageBox.Show("ürün Silindi.");
                    LoadProducts();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
               
            }
        
        }
    }
}
