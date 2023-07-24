using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangMyPham
{
    public partial class FormAdmin : Form
    {
        BindingSource salesList = new BindingSource();
        BindingSource billList = new BindingSource();
        BindingSource mb_detailBillList = new BindingSource();
        BindingSource cb_detailBillList = new BindingSource();
        BindingSource mi_detailImportList = new BindingSource();
        BindingSource ci_detailImportList = new BindingSource();
        BindingSource importList = new BindingSource();
        BindingSource customerList = new BindingSource();
        BindingSource employeeList = new BindingSource();
        BindingSource accountList = new BindingSource();
        BindingSource productList = new BindingSource();
        BindingSource customerBillList = new BindingSource();
        
        private AccountBL loginAccount;

        public AccountBL LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.IdPosition); }
        }

        public FormAdmin(AccountBL acc)
        {
            InitializeComponent();
            this.LoginAccount = acc;

            LoadData(acc.IdPosition);
        }

        #region method
        void ChangeAccount(int typeAccount)
        {
            pnMenuEmployee.Enabled = typeAccount == 1;
            pnMenuEmployee.Visible = typeAccount == 1;
            pnMenuAccount.Enabled = typeAccount == 1;
            pnMenuAccount.Visible = typeAccount == 1;
            pnMenuStock.Enabled = typeAccount == 1;
            pnMenuStock.Visible = typeAccount == 1;
            pnMenuStock.Enabled = typeAccount == 1;
            pnMenuStock.Visible = typeAccount == 1;
            pnMenuReport.Enabled = typeAccount == 1;
            pnMenuReport.Visible = typeAccount == 1;
            btnFormManageBill.Enabled = typeAccount == 1;
            btnFormManageBill.Visible = typeAccount == 1;
            btnFormManageImport.Enabled = typeAccount == 1;
            btnFormManageImport.Visible = typeAccount == 1;
            if (typeAccount == 1)
            {
                lbWelcome.Text = "Chào mừng Quản lý của Jolie";
            }
            else
            {
                lbWelcome.Text = "Chào mừng Nhân viên của Jolie";
            }
        }

        void LoadData(int typeAccount)
        {
            dgvSalesList.DataSource = salesList;
            dgvBill_ManageBill.DataSource = billList;
            dgvDetailBill_ManageBill.DataSource = mb_detailBillList;
            dgvDetailBill_CreateBill.DataSource = cb_detailBillList;
            dgvDetailImport_ManageImport.DataSource = mi_detailImportList;
            dgvDetailImport_CreateImport.DataSource = ci_detailImportList;
            dgvImport_ManageImport.DataSource = importList;
            dgvCustomer.DataSource = customerList;
            dgvEmployee.DataSource = employeeList;
            dgvAccount.DataSource = accountList;
            dgvStock.DataSource = productList;
            dgvBill_Customer.DataSource = customerBillList;
            LoadSalesList();
            LoadInfoBill();
            LoadDetailBill_CB();
            LoadDetailImport_CI();
            LoadInfoImport();
            LoadCustomer();
            LoadProductTypeIntoCombobox(cbProductTypeName_CreateImport);
            AddCustomerBinding(); 
            AddCBDetailBillBinding();
            AddCIDeTailImportBinding();
            
            if (typeAccount == 1)
            {
                LoadDateTimePickerBill();
                LoadListBillByDateAndPage(dtpkFromDate.Value, dtpkToDate.Value, Convert.ToInt32(tbxPageReport.Text));
                LoadBill();
                LoadImport();
                LoadEmployee();
                LoadAccount();
                LoadStock();
                LoadPositionIntoCombobox(cbPosition_Employee);
                LoadProductTypeIntoCombobox(cbProductTypeName_Stock);
                AddAccountBinding();
                AddEmployeeBinding();
                AddProductBinding();
                AddBillBinding();
                AddImportBinding();
            }    
        }

        void LoadSalesList()
        {
            salesList.DataSource = BillBL.Instance.GetSalesList();
        }

        void LoadBill()
        {
            billList.DataSource = BillBL.Instance.GetBillList();
            salesList.DataSource = BillBL.Instance.GetBillList();
            try
            {
                lbNumBillMonth.Text = BillBL.Instance.GetNumBillMonth().Rows[0][0].ToString();
                lbNumWeeklySales.Text = BillBL.Instance.GetNumWeeklySales().Rows[0][0].ToString();
                lbNumMonthlySales.Text = BillBL.Instance.GetNumMonthlySales().Rows[0][0].ToString();
            }
            catch
            {
                lbNumBillMonth.Text = "0";
                lbNumWeeklySales.Text = "0";
                lbNumMonthlySales.Text = "0";
            }
        }

        void LoadInfoBill()
        {
            try
            {
                DataTable result = BillBL.Instance.GetInfoBill();
                tbxIdBill_CreateBill.Text = result.Rows[0][0].ToString();
                tbxIdEmployee_CreateBill.Text = result.Rows[0][1].ToString();
                dtpDateBill_CreateBill.Value = (DateTime)result.Rows[0][2];
                lbTotalPriceBill_CreateBill.Text = String.Format("{0} vnđ", result.Rows[0][3].ToString());
                tbxIdBill_CreateBill.Enabled = false;
                tbxIdEmployee_CreateBill.Enabled = false;
                dtpDateBill_CreateBill.Enabled = false;
            }
            catch
            {
                tbxIdBill_CreateBill.Clear();
                tbxIdEmployee_CreateBill.Clear();
                dtpDateBill_CreateBill.Refresh();
                lbTotalPriceBill_CreateBill.Text = "0 vnđ";
                tbxIdBill_CreateBill.Enabled = false;
                tbxIdEmployee_CreateBill.Enabled = true;
                dtpDateBill_CreateBill.Enabled = false;
                tbxCustomerPhone_CreateBill.Clear();
                tbxCustomerName_CreateBill.Clear();
            }
        }

        void LoadDetailBill_CB()
        {
            cb_detailBillList.DataSource = DetailBillBL.Instance.GetCBDetailBillList();
        }

        void LoadDetailImport_CI()
        {
            ci_detailImportList.DataSource = DetailImportBL.Instance.GetCIDetailImportList();
            if (ci_detailImportList.Count > 0)
            {
                tbxProductName_CreateImport.Enabled = false;
                tbxIdProducer_CreateImport.Enabled = false;
                tbxProducerName_CreateImport.Enabled = false;
                cbProductTypeName_CreateImport.Enabled = false;
            }    
        }

        void LoadImport()
        {
            importList.DataSource = ImportBL.Instance.GetImportList();
        }

        void LoadInfoImport()
        {
            try
            {
                DataTable result = ImportBL.Instance.GetInfoImport();
                tbxIdImport_CreateImport.Text = result.Rows[0][0].ToString();
                tbxIdEmployee_CreateImport.Text = result.Rows[0][1].ToString();
                tbxIdSupplier_CreateImport.Text = result.Rows[0][2].ToString();
                tbxSupplierName_CreateImport.Text = result.Rows[0][3].ToString();
                dtpDateImport_CreateImport.Value = (DateTime)result.Rows[0][4];
                lbTotalPriceImport_CreateImport.Text = String.Format("{0} vnđ", result.Rows[0][5].ToString());
                tbxIdImport_CreateImport.Enabled = false;
                tbxIdEmployee_CreateImport.Enabled = false;
                tbxIdSupplier_CreateImport.Enabled = false;
                tbxSupplierName_CreateImport.Enabled = false;
                dtpDateImport_CreateImport.Enabled = false;

            }
            catch
            {
                tbxIdImport_CreateImport.Clear();
                tbxIdEmployee_CreateImport.Clear();
                tbxIdSupplier_CreateImport.Clear();
                tbxSupplierName_CreateImport.Clear();
                dtpDateImport_CreateImport.Refresh();
                lbTotalPriceImport_CreateImport.Text = "0 vnđ";
                tbxIdImport_CreateImport.Enabled = false;
                tbxIdEmployee_CreateImport.Enabled = true;
                tbxIdSupplier_CreateImport.Enabled = true;
                tbxSupplierName_CreateImport.Enabled = false;
                dtpDateImport_CreateImport.Enabled = false;
            }
        }

        void LoadCustomer()
        {
            customerList.DataSource = CustomerBL.Instance.GetCustomerList();
        }

        void LoadEmployee()
        {
            employeeList.DataSource = EmployeeBL.Instance.GetEmployeeList();
        }

        void LoadAccount()
        {
            accountList.DataSource = AccountBL.Instance.GetAccountList();
        }

        void LoadStock()
        {
            productList.DataSource = ProductBL.Instance.GetProductList();
        }

        void LoadPositionIntoCombobox(ComboBox cb)
        {
            cb.DataSource = PositionBL.Instance.GetListPosition();
            cb.DisplayMember = "PositionName";
        }

        void LoadProductTypeIntoCombobox(ComboBox cb)
        {
            cb.DataSource = ProductTypeBL.Instance.GetListProductType();
            cb.DisplayMember = "ProductTypeName";
        }

        void LoadListBillByDateAndPage(DateTime startDate, DateTime endDate, int page)
        {
            try
            {
                dgvReport.DataSource = BillBL.Instance.GetBillListByDateAndPage(startDate, endDate, Convert.ToInt32(tbxPageReport.Text));
                lbNumBill.Text = String.Format("{0} đơn", BillBL.Instance.GetNumBillListByDate(startDate, endDate));
                lbTurnover.Text = String.Format("{0} vnđ", BillBL.Instance.GetTurnoverByDate(startDate, endDate).Rows[0][0].ToString());
                if (lbTurnover.Text == " vnđ")
                {
                    lbTurnover.Text = "0 vnđ";
                }    
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn ngày khác!");
            }
        }

        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }

        void AddAccountBinding()
        {
            tbxUsername_Account.DataBindings.Add(new Binding("Text", dgvAccount.DataSource, "TenDangNhap", true, DataSourceUpdateMode.Never));
            tbxPosition_Account.DataBindings.Add(new Binding("Text", dgvAccount.DataSource, "TenChucVu", true, DataSourceUpdateMode.Never));
        }

        void AddCustomerBinding()
        {
            tbxIdCustomer_Customer.DataBindings.Add(new Binding("Text", dgvCustomer.DataSource, "idKH", true, DataSourceUpdateMode.Never));
            tbxCustomerName_Customer.DataBindings.Add(new Binding("Text", dgvCustomer.DataSource, "TenKH", true, DataSourceUpdateMode.Never));
            tbxCustomerPhone_Customer.DataBindings.Add(new Binding("Text", dgvCustomer.DataSource, "SDT", true, DataSourceUpdateMode.Never));
        }

        void AddEmployeeBinding()
        {
            tbxIdEmployee_Employee.DataBindings.Add(new Binding("Text", dgvEmployee.DataSource, "idNV", true, DataSourceUpdateMode.Never));
            tbxEmployeeName_Employee.DataBindings.Add(new Binding("Text", dgvEmployee.DataSource, "TenNV", true, DataSourceUpdateMode.Never));
            dtpBirthEmployee_Employee.DataBindings.Add(new Binding("Text", dgvEmployee.DataSource, "NgaySinh", true, DataSourceUpdateMode.Never));
            tbxEmployeePhone_Employee.DataBindings.Add(new Binding("Text", dgvEmployee.DataSource, "SDT", true, DataSourceUpdateMode.Never));
            cbPosition_Employee.DataBindings.Add(new Binding("Text", dgvEmployee.DataSource, "TenChucVu", true, DataSourceUpdateMode.Never));
        }

        void AddProductBinding()
        {
            tbxIdProduct_Stock.DataBindings.Add(new Binding("Text", dgvStock.DataSource, "idHH", true, DataSourceUpdateMode.Never));
            tbxProductName_Stock.DataBindings.Add(new Binding("Text", dgvStock.DataSource, "TenHH", true, DataSourceUpdateMode.Never));
            tbxProducerName_Stock.DataBindings.Add(new Binding("Text", dgvStock.DataSource, "TenNSX", true, DataSourceUpdateMode.Never));
            cbProductTypeName_Stock.DataBindings.Add(new Binding("Text", dgvStock.DataSource, "TenLoai", true, DataSourceUpdateMode.Never));
            tbxPrice_Stock.DataBindings.Add(new Binding("Text", dgvStock.DataSource, "Gia", true, DataSourceUpdateMode.Never));
            tbxInventory_Stock.DataBindings.Add(new Binding("Text", dgvStock.DataSource, "SoLuongTonKho", true, DataSourceUpdateMode.Never));
        }

        void AddBillBinding()
        {
            tbxIdBill_ManageBill.DataBindings.Add(new Binding("Text", dgvBill_ManageBill.DataSource, "idDH", true, DataSourceUpdateMode.Never));
            tbxIdEmployee_ManageBill.DataBindings.Add(new Binding("Text", dgvBill_ManageBill.DataSource, "idNV", true, DataSourceUpdateMode.Never));
            tbxIdCustomer_ManageBill.DataBindings.Add(new Binding("Text", dgvBill_ManageBill.DataSource, "idKH", true, DataSourceUpdateMode.Never));
            dtpDateBill_ManageBill.DataBindings.Add(new Binding("Text", dgvBill_ManageBill.DataSource, "NgayLenDon", true, DataSourceUpdateMode.Never));
        }

        void AddCBDetailBillBinding()
        {
            tbxIdProduct_CreateBill.DataBindings.Add(new Binding("Text", dgvDetailBill_CreateBill.DataSource, "idHH", true, DataSourceUpdateMode.Never));
            tbxProductName_CreateBill.DataBindings.Add(new Binding("Text", dgvDetailBill_CreateBill.DataSource, "TenHH", true, DataSourceUpdateMode.Never));
            tbxUnitPrice_CreateBill.DataBindings.Add(new Binding("Text", dgvDetailBill_CreateBill.DataSource, "DonGia", true, DataSourceUpdateMode.Never));
            tbxQuantity_CreateBill.DataBindings.Add(new Binding("Text", dgvDetailBill_CreateBill.DataSource, "SoLuong", true, DataSourceUpdateMode.Never));
        }

        void AddImportBinding()
        {
            tbxIdImport_ManageImport.DataBindings.Add(new Binding("Text", dgvImport_ManageImport.DataSource, "idPN", true, DataSourceUpdateMode.Never));
            dtpDateImport_ManageImport.DataBindings.Add(new Binding("Text", dgvImport_ManageImport.DataSource, "NgayNhap", true, DataSourceUpdateMode.Never));
            tbxIdEmployee_ManageImport.DataBindings.Add(new Binding("Text", dgvImport_ManageImport.DataSource, "idNV", true, DataSourceUpdateMode.Never));
            tbxIdSupplier_ManageImport.DataBindings.Add(new Binding("Text", dgvImport_ManageImport.DataSource, "idNCC", true, DataSourceUpdateMode.Never));
            
        }
        void AddCIDeTailImportBinding()
        {
            tbxIdProduct_CreateImport.DataBindings.Add(new Binding("Text", dgvDetailImport_CreateImport.DataSource, "idHH", true, DataSourceUpdateMode.Never));
            tbxProductName_CreateImport.DataBindings.Add(new Binding("Text", dgvDetailImport_CreateImport.DataSource, "TenHH", true, DataSourceUpdateMode.Never));
            tbxIdProducer_CreateImport.DataBindings.Add(new Binding("Text", dgvDetailImport_CreateImport.DataSource, "idNSX", true, DataSourceUpdateMode.Never));
            tbxProducerName_CreateImport.DataBindings.Add(new Binding("Text", dgvDetailImport_CreateImport.DataSource, "TenNSX", true, DataSourceUpdateMode.Never));
            cbProductTypeName_CreateImport.DataBindings.Add(new Binding("Text", dgvDetailImport_CreateImport.DataSource, "TenLoai", true, DataSourceUpdateMode.Never));
            tbxUnitPrice_CreateImport.DataBindings.Add(new Binding("Text", dgvDetailImport_CreateImport.DataSource, "DonGia", true, DataSourceUpdateMode.Never));
            tbxQuantity_CreateImport.DataBindings.Add(new Binding("Text", dgvDetailImport_CreateImport.DataSource, "SoLuong", true, DataSourceUpdateMode.Never));
        }

        void InsertBill(DateTime dateBill, int idEmployee)
        {
            if (BillBL.Instance.InsertBill(dateBill, idEmployee))
            {
                MessageBox.Show("Thêm đơn hàng thành công");
            }
            else
            {
                MessageBox.Show("Thêm đơn hàng thất bại");
            }

            LoadInfoBill();
            LoadBill();
            LoadDetailBill_CB();
        }

        void InsertImport(int idEmployee, int idSupplier, DateTime dateImport)
        {
            if (ImportBL.Instance.InsertImport(idEmployee, idSupplier, dateImport))
            {
                MessageBox.Show("Thêm phiếu nhập thành công");
            }
            else
            {
                MessageBox.Show("Thêm phiếu nhập thất bại");
            }

            LoadInfoImport();
        }

        void InsertDetailBill(int idBill, int idProduct, int quantity)
        {
            if (DetailBillBL.Instance.InsertDetailBill(idBill, idProduct, quantity))
            {
                MessageBox.Show("Thêm chi tiết đơn hàng thành công");
            }
            else
            {
                MessageBox.Show("Thêm chi tiết đơn hàng thất bại");
            }

            LoadDetailBill_CB();
            LoadInfoBill();
        }

        void InsertDetailImport(int idImport, int idProduct, string productName, int idProducer, string productTypeName, int quantity, float price)
        {
            if (DetailImportBL.Instance.InsertDetailImport(idImport, idProduct, productName, idProducer, productTypeName, quantity, price))
            {
                MessageBox.Show("Thêm chi tiết phiếu nhập thành công");
            }
            else
            {
                MessageBox.Show("Thêm chi tiết phiếu nhập thất bại");
            }

            LoadDetailImport_CI();
            LoadInfoImport();
        }

        void UpdateDetailBill(int idBill, int idProduct, int quantity)
        {
            if (DetailBillBL.Instance.UpdateDetailBill(idBill, idProduct, quantity))
            {
                MessageBox.Show("Cập nhật chi tiết đơn hàng thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật chi tiết đơn hàng thất bại");
            }

            LoadDetailBill_CB();
            LoadInfoBill();
        }

        void UpdateDetailImport(int idImport, int idProduct, int quantity, float price)
        {
            if (DetailImportBL.Instance.UpdateDetailImport(idImport, idProduct, quantity, price))
            {
                MessageBox.Show("Cập nhật chi tiết phiếu nhập thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật chi tiết phiếu nhập thất bại");
            }

            LoadDetailImport_CI();
            LoadInfoImport();
        }

        void DeleteDetailBill(int idBill, int idProduct)
        {
            if (DetailBillBL.Instance.DeleteDetailBill(idBill, idProduct))
            {
                MessageBox.Show("Xóa chi tiết đơn hàng thành công");
            }
            else
            {
                MessageBox.Show("Xóa chi tiết đơn hàng thất bại");
            }

            LoadDetailBill_CB();
            LoadInfoBill();
        }

        void DeleteDetailImport(int idImport, int idProduct)
        {
            try
            {
                if (DetailImportBL.Instance.DeleteDetailImport(idImport, idProduct))
                {
                    MessageBox.Show("Xóa chi tiết phiếu nhập thành công");
                    if(ProductBL.Instance.DeleteProductCancelImport(idProduct))
                    {
                        MessageBox.Show("Xóa hàng hóa thành công");
                    }
                    else
                    {
                        MessageBox.Show("Xóa hàng hóa thất bại");
                    }
                }
                else
                {
                    MessageBox.Show("Xóa chi tiết phiếu nhập thất bại");
                }
            }
            catch { }

            LoadDetailImport_CI();
            LoadInfoImport();
            LoadStock();
        }

        void CheckOutCB(int idBill, string customerPhone)
        {
            if (BillBL.Instance.CheckOut(idBill, customerPhone))
            {
                MessageBox.Show("Thanh toán đơn hàng thành công");
            }
            else
            {
                MessageBox.Show("Thanh toán đơn hàng thất bại");
            }

            LoadDetailBill_CB();
            LoadInfoBill();
            LoadBill();
            LoadStock();
        }

        void CheckOutCI(int idImport)
        {
            if (ImportBL.Instance.CheckOut(idImport))
            {
                MessageBox.Show("Nhập hàng thành công");
            }
            else
            {
                MessageBox.Show("Nhập hàng thất bại");
            }

            LoadDetailImport_CI();
            LoadInfoImport();
            LoadImport();
            LoadStock();
        }

        void DeleteBill(int idBill)
        {
            try
            {
                if (BillBL.Instance.DeleteBill(idBill))
                {
                    MessageBox.Show("Xóa đơn hàng thành công");
                }
                else
                {
                    MessageBox.Show("Xóa đơn hàng thất bại");
                }
            }
            catch { }

            LoadDetailBill_CB();
            LoadInfoBill();
            LoadProductTypeIntoCombobox(cbProductTypeName_CreateImport);
            LoadBill();
        }

        void DeleteImport(int idImport)
        {
            try
            {
                if (ImportBL.Instance.DeleteImport(idImport))
                {
                    MessageBox.Show("Xóa phiếu nhập thành công");
                }
                else
                {
                    MessageBox.Show("Xóa phiếu nhập thất bại");
                }
            }
            catch { }

            LoadDetailImport_CI();
            LoadInfoImport();
            LoadImport();
            LoadStock();
        }

        void SearchBill(int idBill)
        {
            billList.DataSource = BillBL.Instance.SearchBill(idBill);
        }

        void SearchImport(int idImport)
        {
            importList.DataSource = ImportBL.Instance.SearchImport(idImport);
        }

        void SearchCustomer(string customerPhone)
        {
            customerList.DataSource = CustomerBL.Instance.SearchCustomer(customerPhone);
        }

        void SearchEmployee(int idEmployee)
        {
            employeeList.DataSource = EmployeeBL.Instance.SearchEmployee(idEmployee);
        }

        void SearchProduct(int idProduct)
        {
            productList.DataSource = ProductBL.Instance.SearchProduct(idProduct);
        }

        void UpdateProduct(int idProduct, float price, string productTypeName)
        {
            if (ProductBL.Instance.UpdateProduct(idProduct, price, productTypeName))
            {
                MessageBox.Show("Cập nhật hàng hóa thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật hàng hóa thất bại");
            }

            LoadStock();
        }

        void ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (AccountBL.Instance.ChangePassword(username, oldPassword, newPassword))
            {
                MessageBox.Show("Đổi mật khẩu thành công");
            }
            else
            {
                MessageBox.Show("Đổi mật khẩu thất bại");
            }

            LoadAccount();
        }

        void ResetPassword(string username)
        {
            if (AccountBL.Instance.ResetPassword(username))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại");
            }

            LoadAccount();
        }

        void InsertEmployee(string employeeName, DateTime birthEmployee, string employeePhone, string position)
        {
            if (EmployeeBL.Instance.InsertEmployee(employeeName, birthEmployee, employeePhone, position))
            {
                MessageBox.Show("Thêm nhân viên thành công");
            }
            else
            {
                MessageBox.Show("Thêm nhân viên thất bại");
            }

            LoadEmployee();
        }

        void UpdateEmployee(int idEmployee, string employeeName, DateTime birthEmployee, string employeePhone, string position)
        {
            if (EmployeeBL.Instance.UpdateEmployee(idEmployee, employeeName, birthEmployee, employeePhone, position))
            {
                MessageBox.Show("Cập nhật thông tin nhân viên thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật thông tin nhân viên thất bại");
            }

            LoadEmployee();
        }

        void DeleteEmployee(int idEmployee)
        {
            if (EmployeeBL.Instance.DeleteEmployee(idEmployee))
            {
                MessageBox.Show("Xóa nhân viên thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Xóa nhân viên thất bại", "Thông báo");
            }

            LoadEmployee();
            LoadPositionIntoCombobox(cbPosition_Employee);
        }

        void InsertSupplier(int idSupplier, string supplierName, string supplierAddress, string supplierPhone, string supplierEmail)
        {
            if (SupplierBL.Instance.InsertSupplier(idSupplier, supplierName, supplierAddress, supplierPhone, supplierEmail))
            {
                FindSupplier(idSupplier);
                pnAddSupplier_CreateImport.Visible = false;
                pnAddDetailImport_CreateImport.Visible = true;
                MessageBox.Show("Thêm nhà cung cấp thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Thêm nhà cung cấp thất bại", "Thông báo");
            }          
        }

        void InsertProducer(int idProducer, string producerName, string producerNation)
        {
            if (ProducerBL.Instance.InsertProducer(idProducer, producerName, producerNation))
            {
                FindProducer(idProducer);
                pnAddProducer_CreateImport.Visible = false;
                tbxProducerName_CreateImport.Enabled = false;
                MessageBox.Show("Thêm nhà sản xuất thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Thêm nhà sản xuất thất bại", "Thông báo");
            }
        }

        void FindSupplier(int idSupplier)
        {
            try
            {
                tbxSupplierName_CreateImport.Text = SupplierBL.Instance.FindSupplier(idSupplier);
            }
            catch
            {
                string message = "Chưa có thông tin của nhà cung cấp này trong hệ thống!\nBạn có muốn thêm nhà cung cấp mới không?";
                string title = "Thông báo";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    pnAddSupplier_CreateImport.Visible = true;
                    tbxIdSupplier_CreateImport.Leave -= tbxIdSupplier_CreateImport_Leave;
                    pnAddDetailImport_CreateImport.Visible = false;
                    pnAddSupplier_CreateImport.BringToFront();
                    tbxSupplierName_AddSupplier.Focus();
                    tbxIdSupplier_CreateImport.Leave += tbxIdSupplier_CreateImport_Leave;
                }
                else
                {
                    tbxIdSupplier_CreateImport.Clear();
                    tbxIdSupplier_CreateImport.Focus();
                }
            }
        }

        void FindEmployeeCB(int idEmployee)
        {
            try
            {
                string employeeName = EmployeeBL.Instance.FindEmployee(idEmployee);
                string message = String.Format("Vui lòng xác nhận đây có phải là thông tin của bạn không?\nMã nhân viên: {0}\nTên nhân viên: {1}", idEmployee, employeeName);
                string title = "Thông báo";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    tbxIdEmployee_CreateBill.Enabled = false;
                }
                else
                {
                    tbxIdEmployee_CreateBill.Clear();
                    tbxIdEmployee_CreateBill.Focus();
                }
            }
            catch
            {
                string message = "Không có thông tin của nhân viên này trong hệ thống!\nVui lòng nhập lại mã nhân viên!";
                string title = "Thông báo";
                MessageBoxButtons buttons = MessageBoxButtons.OK;

                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    tbxIdEmployee_CreateBill.Clear();
                    tbxIdEmployee_CreateBill.Focus();
                }
            }
        }

        void FindEmployeeCI(int idEmployee)
        {
            try
            {
                string employeeName = EmployeeBL.Instance.FindEmployee(idEmployee);
                string message = String.Format("Vui lòng xác nhận đây có phải là thông tin của bạn không?\nMã nhân viên: {0}\nTên nhân viên: {1}", idEmployee, employeeName);
                string title = "Thông báo";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    tbxIdEmployee_CreateImport.Enabled = false;
                }
                else
                {
                    tbxIdEmployee_CreateImport.Clear();
                    tbxIdEmployee_CreateImport.Focus();
                }
            }
            catch
            {
                string message = "Không có thông tin của nhân viên này trong hệ thống!\nVui lòng nhập lại mã nhân viên!";
                string title = "Thông báo";
                MessageBoxButtons buttons = MessageBoxButtons.OK;

                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    tbxIdEmployee_CreateImport.Clear();
                    tbxIdEmployee_CreateImport.Focus();
                }
            }
        }

        void FindCustomer(string customerPhone)
        {
            try
            {
                tbxCustomerName_CreateBill.Text = CustomerBL.Instance.FindCustomer(customerPhone);
                tbxCustomerName_CreateBill.Enabled = false;
            }
            catch
            {
                string message = "Không có thông tin của khách hàng này trong hệ thống!\nBạn có muốn thêm mới khách hàng này không?";
                string title = "Thông báo";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {
                    tbxCustomerName_CreateBill.Focus();
                    tbxCustomerName_CreateBill.Enabled = true;
                }
                else
                {
                    tbxCustomerPhone_CreateBill.Clear();
                    tbxCustomerPhone_CreateBill.Focus();
                    tbxCustomerName_CreateBill.Enabled = false;
                }    
            }
        }

        void InsertCustomer(string customerPhone, string customerName)
        {
            try
            {
                if (CustomerBL.Instance.InsertCustomer(customerPhone, customerName))
                {
                    tbxCustomerName_Customer.Text = CustomerBL.Instance.FindCustomer(customerPhone);
                    tbxCustomerName_CreateBill.Enabled = false;
                    MessageBox.Show("Thêm khách hàng thành công", "Thông báo");
                }
            }
            catch
            {
                MessageBox.Show("Thêm khách hàng thất bại", "Thông báo");
                tbxCustomerName_CreateBill.Enabled = true;
            }
            LoadCustomer();
        }

        void FindProductCB(int idProduct)
        {
            try
            {
                DataTable result = ProductBL.Instance.FindProductCB(idProduct);
                tbxProductName_CreateBill.Text = result.Rows[0][0].ToString();
                tbxUnitPrice_CreateBill.Text = result.Rows[0][1].ToString();
            }
            catch
            {
                MessageBox.Show("Không có hàng hóa này trong hệ thống!\nVui lòng nhập lại!");
                tbxIdProduct_CreateBill.Clear();
                tbxIdProduct_CreateBill.Focus();
            }
        }

        void FindProductCI(int idProduct)
        {
            try
            {
                DataTable result = ProductBL.Instance.FindProductCI(idProduct);
                tbxProductName_CreateImport.Text = result.Rows[0][0].ToString();
                tbxIdProducer_CreateImport.Text = result.Rows[0][1].ToString();
                tbxProducerName_CreateImport.Text = result.Rows[0][2].ToString();
                cbProductTypeName_CreateImport.Text = result.Rows[0][3].ToString();
                tbxProductName_CreateImport.Enabled = false;
                tbxIdProducer_CreateImport.Enabled = false;
                tbxProducerName_CreateImport.Enabled = false;
                cbProductTypeName_CreateImport.Enabled = false;
            }
            catch
            {
                string message = String.Format("Không có hàng hóa này trong hệ thống!\nVui lòng kiểm tra lại mã hàng hóa!\n Mã hàng hóa: {0}\n Bạn chắc chắn đã nhập đúng mã hàng hóa chứ?", idProduct);
                string title = "Thông báo";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    tbxProductName_CreateImport.Enabled = true;
                    cbProductTypeName_CreateImport.Enabled = true;
                    tbxIdProducer_CreateImport.Enabled = true;
                }
                else
                {
                    tbxProductName_CreateImport.Enabled = false;
                    cbProductTypeName_CreateImport.Enabled = false;
                    tbxIdProduct_CreateImport.Clear();
                    tbxIdProduct_CreateImport.Focus();
                }
            }
        }

        void FindProducer(int idProducer)
        {
            try
            {
                tbxProducerName_CreateImport.Text = ProducerBL.Instance.FindProducer(idProducer).Rows[0][0].ToString();
                tbxProducerName_CreateImport.Enabled = false;
            }
            catch
            {
                string message = "Chưa có thông tin của nhà sản xuất này trong hệ thống!\nBạn có muốn thêm nhà sản xuất mới không?";
                string title = "Thông báo";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    pnAddProducer_CreateImport.Visible = true;
                    pnAddProducer_CreateImport.BringToFront();
                }
                else
                {
                    tbxIdProducer_CreateImport.Clear();
                    tbxIdProducer_CreateImport.Focus();
                }
            }
        }
        #endregion

        #region event
        private void pbExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }    
            
        }

        private void lbMenuDashboard_Click(object sender, EventArgs e)
        {
            this.pnFormDashboard.Visible = true;
            this.pnFormBill.Visible = false;
            this.pnFormImport.Visible = false;
            this.pnFormCustomer.Visible = false;
            this.pnFormEmployee.Visible = false;
            this.pnFormAccount.Visible = false;
            this.pnFormStock.Visible = false;
            this.pnFormReport.Visible = false;
        }

        private void lbMenuBill_Click(object sender, EventArgs e)
        {
            this.pnFormBill.VisibleChanged -= pnFormBill_VisibleChanged;
            this.pnFormDashboard.Visible = false;
            this.pnFormImport.Visible = false;
            this.pnFormCustomer.Visible = false;
            this.pnFormEmployee.Visible = false;
            this.pnFormAccount.Visible = false;
            this.pnFormStock.Visible = false;
            this.pnFormReport.Visible = false;
            this.pnFormBill.Visible = true;

            this.btnFormCreateBill.FlatStyle = FlatStyle.Standard;

            this.pnFormCreateBill.Visible = true;
            this.pnFormManageBill.Visible = false;
            this.pnFormBill.VisibleChanged += pnFormBill_VisibleChanged;
        }

        private void lbMenuImport_Click(object sender, EventArgs e)
        {
            this.pnFormImport.VisibleChanged -= pnFormImport_VisibleChanged;
            this.pnFormDashboard.Visible = false;
            this.pnFormBill.Visible = false;
            this.pnFormCustomer.Visible = false;
            this.pnFormEmployee.Visible = false;
            this.pnFormAccount.Visible = false;
            this.pnFormStock.Visible = false;
            this.pnFormReport.Visible = false;
            this.pnFormImport.Visible = true;
            this.pnFormImport.VisibleChanged += pnFormImport_VisibleChanged;
        }

        private void lbMenuCustomer_Click(object sender, EventArgs e)
        {
            this.pnFormDashboard.Visible = false;
            this.pnFormBill.Visible = false;
            this.pnFormImport.Visible = false;
            this.pnFormCustomer.Visible = true;
            this.pnFormEmployee.Visible = false;
            this.pnFormAccount.Visible = false;
            this.pnFormStock.Visible = false;
            this.pnFormReport.Visible = false;
        }

        private void lbMenuEmployee_Click(object sender, EventArgs e)
        {
            this.pnFormDashboard.Visible = false;
            this.pnFormBill.Visible = false;
            this.pnFormImport.Visible = false;
            this.pnFormCustomer.Visible = false;
            this.pnFormEmployee.Visible = true;
            this.pnFormAccount.Visible = false;
            this.pnFormStock.Visible = false;
            this.pnFormReport.Visible = false;
        }
        private void lbMenuAccount_Click(object sender, EventArgs e)
        {
            this.pnFormDashboard.Visible = false;
            this.pnFormBill.Visible = false;
            this.pnFormImport.Visible = false;
            this.pnFormCustomer.Visible = false;
            this.pnFormEmployee.Visible = false;
            this.pnFormAccount.Visible = true;
            this.pnFormStock.Visible = false;
            this.pnFormReport.Visible = false;
        }

        private void lbMenuStock_Click(object sender, EventArgs e)
        {
            this.pnFormDashboard.Visible = false;
            this.pnFormBill.Visible = false;
            this.pnFormImport.Visible = false;
            this.pnFormCustomer.Visible = false;
            this.pnFormEmployee.Visible = false;
            this.pnFormAccount.Visible = false;
            this.pnFormStock.Visible = true;
            this.pnFormReport.Visible = false;
        }

        private void lbMenuReport_Click(object sender, EventArgs e)
        {
            this.pnFormDashboard.Visible = false;
            this.pnFormBill.Visible = false;
            this.pnFormImport.Visible = false;
            this.pnFormCustomer.Visible = false;
            this.pnFormEmployee.Visible = false;
            this.pnFormAccount.Visible = false;
            this.pnFormStock.Visible = false;
            this.pnFormReport.Visible = true;
        }

        private void btnFormCreateBill_Click(object sender, EventArgs e)
        {
            this.btnFormCreateBill.FlatStyle = FlatStyle.Standard;
            this.btnFormManageBill.FlatStyle = FlatStyle.Flat;

            this.pnFormCreateBill.Visible = true;
            this.pnFormManageBill.Visible = false;
        }

        private void btnFormManageBill_Click(object sender, EventArgs e)
        {
            this.btnFormManageBill.FlatStyle = FlatStyle.Standard;
            this.btnFormCreateBill.FlatStyle = FlatStyle.Flat;

            this.pnFormManageBill.Visible = true;
            this.pnFormCreateBill.Visible = false;
        }

        private void btnCreateImport_Click(object sender, EventArgs e)
        {
            this.btnFormCreateImport.FlatStyle = FlatStyle.Standard;
            this.btnFormManageImport.FlatStyle = FlatStyle.Flat;

            this.pnFormCreateImport.Visible = true;
            this.pnFormManageImport.Visible = false;
        }

        private void btnManageImport_Click(object sender, EventArgs e)
        {
            this.btnFormManageImport.FlatStyle = FlatStyle.Standard;
            this.btnFormCreateImport.FlatStyle = FlatStyle.Flat;

            this.pnFormManageImport.Visible = true;
            this.pnFormCreateImport.Visible = false;
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            this.pnFormDashboard.Visible = true;
            this.pnFormBill.Visible = false;
            this.pnFormImport.Visible = false;
            this.pnFormCustomer.Visible = false;
            this.pnFormEmployee.Visible = false;
            this.pnFormAccount.Visible = false;
            this.pnFormStock.Visible = false;
            this.pnFormReport.Visible = false;
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            if (productList.Count > 0)
            {
                try
                {
                    int idProduct = Convert.ToInt32(tbxIdProduct_Stock.Text.Trim());
                    float price = (float)Convert.ToDouble(tbxPrice_Stock.Text.Trim());
                    string productTypeName = cbProductTypeName_Stock.Text;
                    if (price != 0 && price.ToString() != "")
                    {
                        UpdateProduct(idProduct, price, productTypeName);
                    }    
                    else
                    {
                        MessageBox.Show("Vui lòng điền đầy đủ các thông tin với các giá trị hợp lệ!");
                    }    
                }
                catch
                {
                    MessageBox.Show("Vui lòng điền đầy đủ các thông tin với các giá trị hợp lệ!");
                }
            }    
            else
            {
                MessageBox.Show("Không có thông tin của hàng hóa nào để cập nhật!");
            }    
        }

        private void dgvBill_ManageBill_CellClick(object sender, DataGridViewCellEventArgs e)
        { 
            int idBill = Convert.ToInt32(tbxIdBill_ManageBill.Text);
            mb_detailBillList.DataSource = DetailBillBL.Instance.GetMBDetailBillList(idBill);
            lbTotalPriceBill_ManageBill.Text = String.Format("{0} vnđ", BillBL.Instance.GetTotalBill(idBill).Rows[0][0].ToString());
        }

        private void btnAddDetailBill_CreateBill_Click(object sender, EventArgs e)
        {
            int idBill = 0;
            try
            {
                idBill = Convert.ToInt32(tbxIdBill_CreateBill.Text);
            }
            catch
            {
                idBill = 0;
            }
            try
            {
                int idEmployee = Convert.ToInt32(tbxIdEmployee_CreateBill.Text.Trim());
                int idProduct = Convert.ToInt32(tbxIdProduct_CreateBill.Text.Trim());
                int quantity = Convert.ToInt32(tbxQuantity_CreateBill.Text.Trim());
                DateTime dateBill = dtpDateBill_CreateBill.Value;
                if (quantity == 0)
                {
                    MessageBox.Show("Vui lòng điền số lương hàng hóa!");
                }
                else if (idBill.ToString() == "0")
                {
                    InsertBill(dateBill, idEmployee);
                    idBill = Convert.ToInt32(tbxIdBill_CreateBill.Text);
                    InsertDetailBill(idBill, idProduct, quantity);
                }
                else
                {
                    InsertDetailBill(idBill, idProduct, quantity);
                }
            }
            catch
            {
                MessageBox.Show("Vui lòng điền đầy đủ các thông tin!");
            }
            
        }

        private void btnUpdateDetailBill_CreateBill_Click(object sender, EventArgs e)
        {
            if (cb_detailBillList.Count > 0)
            {
                int idBill = Convert.ToInt32(tbxIdBill_CreateBill.Text.Trim());
                int idProduct = Convert.ToInt32(tbxIdProduct_CreateBill.Text.Trim());
                int quantity = Convert.ToInt32(tbxQuantity_CreateBill.Text.Trim());
                if (quantity == 0)
                {
                    MessageBox.Show("Vui lòng điền số lương hàng hóa!");
                }
                else
                {
                    UpdateDetailBill(idBill, idProduct, quantity);
                }
            }    
            else
            {
                MessageBox.Show("Chưa có hàng hóa nào để cập nhật thông tin!");
            }    
        }

        private void dgvImport_ManageImport_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idImport = Convert.ToInt32(tbxIdImport_ManageImport.Text);
            mi_detailImportList.DataSource = DetailImportBL.Instance.GetMIDetailImportList(idImport);
            lbTotalPriceImport_ManageImport.Text = String.Format("{0} vnđ", ImportBL.Instance.GetTotalImport(idImport).Rows[0][0].ToString());
        }

        private void dgvBill_Customer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idCustomer = Convert.ToInt32(tbxIdCustomer_Customer.Text);
            customerBillList.DataSource = CustomerBL.Instance.GetCustomerBillList(idCustomer);
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idCustomer = Convert.ToInt32(tbxIdCustomer_Customer.Text);
            customerBillList.DataSource = CustomerBL.Instance.GetCustomerBillList(idCustomer);
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                string username = tbxUsername_Account.Text;
                string oldPassword = tbxPassword_Account.Text;
                string newPassword = tbxNewPassword_Account.Text;
                string confirmPassword = tbxConfirmPassword_Account.Text;
                if (newPassword != oldPassword && newPassword == confirmPassword)
                {
                    try
                    {
                        ChangePassword(username, oldPassword, newPassword);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (newPassword == oldPassword)
                {
                    MessageBox.Show("Mật khẩu mới trùng mới mật khẩu cũ!");
                }
                else
                {
                    MessageBox.Show("Mật khẩu mới không khớp!");
                }
            }
            catch
            {
                MessageBox.Show("Vui lòng điền đầy đủ các thông tin!");
            }
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                string employeeName = tbxEmployeeName_Employee.Text.Trim();
                DateTime birthEmployee = dtpBirthEmployee_Employee.Value;
                string employeePhone = tbxEmployeePhone_Employee.Text.Trim();
                string position = cbPosition_Employee.Text.Trim();
                if (employeeName != "" && employeePhone != "")
                {
                    InsertEmployee(employeeName, birthEmployee, employeePhone, position);
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đầy đủ các thông tin!");
                }
            }
            catch
            {
                MessageBox.Show("Vui lòng điền đầy đủ các thông tin!");
            }
        }

        private void btnUpdateEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                int idEmployee = Convert.ToInt32(tbxIdEmployee_Employee.Text.Trim());
                string employeeName = tbxEmployeeName_Employee.Text.Trim();
                DateTime birthEmployee = dtpBirthEmployee_Employee.Value;
                string employeePhone = tbxEmployeePhone_Employee.Text.Trim();
                string position = cbPosition_Employee.Text.Trim();
                if (employeeName != "" && employeePhone != "")
                {
                    UpdateEmployee(idEmployee, employeeName, birthEmployee, employeePhone, position);
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đầy đủ các thông tin!");
                }
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn nhân viên để cập nhật thông tin!");
            }
        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                int idEmployee = Convert.ToInt32(tbxIdEmployee_Employee.Text.Trim());
                string message = "Bạn có chắc chắn muốn xóa nhân viên này không?";
                string title = "Thông báo";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    DeleteEmployee(idEmployee);
                }
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa!");
            }
        }

        private void tbxIdProduct_CreateBill_Leave(object sender, EventArgs e)
        {
            try
            {
                int idProduct = Convert.ToInt32(tbxIdProduct_CreateBill.Text.Trim());
                FindProductCB(idProduct);
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập giá trị hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                tbxIdProduct_CreateBill.Focus();
            }
        }

        private void tbxIdSupplier_CreateImport_Leave(object sender, EventArgs e)
        {
            try
            {
                int idSupplier = Convert.ToInt32(tbxIdSupplier_CreateImport.Text.Trim());
                FindSupplier(idSupplier);
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập giá trị hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                tbxIdSupplier_CreateImport.Focus();
            }
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                int idSupplier = Convert.ToInt32(tbxIdSupplier_CreateImport.Text.Trim());
                string supplierName = tbxSupplierName_AddSupplier.Text.Trim();
                string supplierAddress = tbxSupplierAddress_AddSupplier.Text.Trim();
                string supplierPhone = tbxSupplierPhone_AddSupplier.Text.Trim();
                string supplierEmail = tbxSupplierEmail_AddSupplier.Text.Trim();
                if (idSupplier.ToString() != "" && supplierName != "" && supplierAddress != "" && supplierPhone != "" && supplierEmail != "")
                {
                    InsertSupplier(idSupplier, supplierName, supplierAddress, supplierPhone, supplierEmail);
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đầy đủ các thông tin!");
                }
            }
            catch
            {
                MessageBox.Show("Vui lòng điền đầy đủ các thông tin!");
            }
        }

        private void btnCancelAddSupplier_Click(object sender, EventArgs e)
        {
            tbxIdSupplier_CreateImport.Clear();
            tbxIdSupplier_CreateImport.Focus();
            pnAddSupplier_CreateImport.Visible = false;
            pnAddDetailImport_CreateImport.Visible = true;
        }
                
        private void tbxIdEmployee_CreateBill_Leave(object sender, EventArgs e)
        {
            try
            {
                int idEmployee = Convert.ToInt32(tbxIdEmployee_CreateBill.Text.Trim());
                FindEmployeeCB(idEmployee);
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập giá trị hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                tbxIdEmployee_CreateBill.Focus();
            }
        }

        private void tbxCustomerPhone_CreateBill_Leave(object sender, EventArgs e)
        {
            try
            {
                string customerPhone = tbxCustomerPhone_CreateBill.Text.Trim();
                FindCustomer(customerPhone);
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập giá trị hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                tbxCustomerPhone_CreateBill.Focus();
            }
        }

        private void btnAddCustomer_CreateBill_Click(object sender, EventArgs e)
        {
            try
            {
                string customerPhone = tbxCustomerPhone_CreateBill.Text.Trim();
                string customerName = tbxCustomerName_CreateBill.Text.Trim();
                InsertCustomer(customerPhone, customerName);
            }
            catch
            {
                MessageBox.Show("Vui lòng điền đầy đủ các thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }    
        }

        private void btnSetUnknownCustomer_Click(object sender, EventArgs e)
        {
            string message = "Bạn có chắc chắn muốn tạo đơn hàng này cho 'Khách lẻ' không?";
            string title = "Thông báo";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                tbxCustomerPhone_CreateBill.Text = "0000000000";
                tbxCustomerPhone_CreateBill_Leave(sender, e);
                btnCheckOut_CreateBill.Focus();
            }
        }

        private void btnDeleteDetailBill_CreateBill_Click(object sender, EventArgs e)
        {
            try
            {
                int idBill = Convert.ToInt32(tbxIdBill_CreateBill.Text.Trim());
                int idProduct = Convert.ToInt32(tbxIdProduct_CreateBill.Text.Trim());
                string message = "Bạn có chắc chắn muốn xóa hàng hóa này khỏi đơn hàng không?";
                string title = "Thông báo";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    DeleteDetailBill(idBill, idProduct);
                }
            }
            catch
            {
                MessageBox.Show("Không thể xóa chi tiết đơn hàng do đơn hàng chưa có hàng hóa nào!\nVui lòng thêm chi tiết đơn hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btnCheckOut_CreateBill_Click(object sender, EventArgs e)
        {
            try
            {
                int idBill = Convert.ToInt32(tbxIdBill_CreateBill.Text.Trim());
                int idEmployee = Convert.ToInt32(tbxIdEmployee_CreateBill.Text.Trim());
                DateTime dateBill = dtpDateBill_CreateBill.Value;
                string customerName = tbxCustomerName_CreateBill.Text.Trim();
                string customerPhone = tbxCustomerPhone_CreateBill.Text.Trim();
                string totalBill = lbTotalPriceBill_CreateBill.Text;
                string message = String.Format("Mã đơn hàng: {0}\nMã nhân viên: {1}\nNgày lên đơn: {2}\nKhách hàng: {3}\nSố điện thoại: {4}\nTổng giá trị: {5}\nBạn có chắc chắn muốn thanh toán đơn hàng này không?", idBill, idEmployee, dateBill, customerName, customerPhone, totalBill);
                string title = "Xác nhận thanh toán";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                if (totalBill != "0 vnđ")
                {
                    if (customerName == "")
                    {
                        MessageBox.Show("Vui lòng nhập thông tin khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show(message, title, buttons);
                        if (result == DialogResult.Yes)
                        {
                            CheckOutCB(idBill, customerPhone);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Đơn hàng chưa có hàng hóa nào!\nVui lòng thêm chi tiết đơn hàng trước khi thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }    
            }
            catch
            {
                MessageBox.Show("Không có đơn hàng nào để thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btnCancel_CreateBill_Click(object sender, EventArgs e)
        {
            try
            {
                int idBill = Convert.ToInt32(tbxIdBill_CreateBill.Text.Trim());
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn hủy đơn hàng này không? Thao tác này sẽ xóa tất cả các thông tin về đơn hàng này!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
                if (result == DialogResult.Yes)
                {
                    DeleteBill(idBill);
                }
            }
            catch
            {
                MessageBox.Show("Không có đơn hàng nào để hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btnDeleteBill_ManageBill_Click(object sender, EventArgs e)
        {
            try
            {
                int idBill = Convert.ToInt32(tbxIdBill_ManageBill.Text.Trim());
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa đơn hàng này không? Thao tác này sẽ xóa tất cả các thông tin về đơn hàng này!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
                if (result == DialogResult.Yes)
                {
                    DeleteBill(idBill);
                }
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn đơn hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            
        }

        private void btnSearchBill_Click(object sender, EventArgs e)
        {
            try
            {
                int idBill = Convert.ToInt32(tbxSearchBill.Text.Trim());
                SearchBill(idBill);
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập giá trị hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                tbxSearchBill.Focus();
            }
        }

        private void tbxIdEmployee_CreateImport_Leave(object sender, EventArgs e)
        {
            try
            {
                int idEmployee = Convert.ToInt32(tbxIdEmployee_CreateImport.Text.Trim());
                FindEmployeeCI(idEmployee);
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập giá trị hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                tbxIdEmployee_CreateImport.Focus();
            }
        }

        private void tbxIdProduct_CreateImport_Leave(object sender, EventArgs e)
        {
            try
            {
                int idProduct = Convert.ToInt32(tbxIdProduct_CreateImport.Text.Trim());
                FindProductCI(idProduct);
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập giá trị hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                tbxIdProduct_CreateImport.Focus();
            }
        }

        private void tbxIdProducer_CreateImport_Leave(object sender, EventArgs e)
        {
            try
            {
                int idProducer = Convert.ToInt32(tbxIdProducer_CreateImport.Text.Trim());
                FindProducer(idProducer);
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập giá trị hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                tbxIdProducer_CreateImport.Focus();
            }
        }

        private void btnCancelAddProducer_CreateImport_Click(object sender, EventArgs e)
        {
            tbxIdProducer_CreateImport.Clear();
            pnAddProducer_CreateImport.ResetText();
            pnAddProducer_CreateImport.Visible = false;
        }

        private void btnAddProducer_Click(object sender, EventArgs e)
        {
            try
            {
                int idProducer = Convert.ToInt32(tbxIdProducer_CreateImport.Text.Trim());
                string producerName = tbxProducerName_AddProducer.Text.Trim();
                string producerNation = tbxProducerNation_AddProducer.Text.Trim();
                if (idProducer.ToString() != "" && producerName != "" && producerNation != "")
                {
                    InsertProducer(idProducer, producerName, producerNation);
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đầy đủ các thông tin!");
                }
            }
            catch
            {
                MessageBox.Show("Vui lòng điền đầy đủ các thông tin!");
            }
        }

        private void btnAddDetailImport_CreateImport_Click(object sender, EventArgs e)
        {
            int idImport = 0;
            try
            {
                idImport = Convert.ToInt32(tbxIdImport_CreateImport.Text);
            }
            catch
            {
                idImport = 0;
            }
            try
            {
                int idEmployee = Convert.ToInt32(tbxIdEmployee_CreateImport.Text);
                int idSupplier = Convert.ToInt32(tbxIdSupplier_CreateImport.Text);
                DateTime dateImport = dtpDateImport_CreateImport.Value;
                int idProduct = Convert.ToInt32(tbxIdProduct_CreateImport.Text);
                string productName = tbxProductName_CreateImport.Text;
                string productTypeName = cbProductTypeName_CreateImport.Text;
                int idProducer = Convert.ToInt32(tbxIdProducer_CreateImport.Text);
                float price = (float)Convert.ToDouble(tbxUnitPrice_CreateImport.Text);
                int quantity = Convert.ToInt32(tbxQuantity_CreateImport.Text);
                if (quantity == 0)
                {
                    MessageBox.Show("Vui lòng điền số lương hàng hóa!");
                }
                else if (idImport.ToString() == "0")
                {
                    InsertImport(idEmployee, idSupplier, dateImport);
                    idImport = Convert.ToInt32(tbxIdImport_CreateImport.Text);
                    InsertDetailImport(idImport, idProduct, productName, idProducer, productTypeName, quantity, price);
                }
                else
                {
                    InsertDetailImport(idImport, idProduct, productName, idProducer, productTypeName, quantity, price);
                }
            }
            catch
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
            }
            
        }

        private void btnUpdateDetailImport_CreateImport_Click(object sender, EventArgs e)
        {
            if (ci_detailImportList.Count > 0)
            {
                int idImport = Convert.ToInt32(tbxIdImport_CreateImport.Text);
                int idProduct = Convert.ToInt32(tbxIdProduct_CreateImport.Text);
                float price = (float)Convert.ToDouble(tbxUnitPrice_CreateImport.Text);
                int quantity = Convert.ToInt32(tbxQuantity_CreateImport.Text);
                if (quantity == 0)
                {
                    MessageBox.Show("Vui lòng điền số lương hàng hóa!");
                }
                else
                {
                    UpdateDetailImport(idImport, idProduct, quantity, price);
                }
            }
            else
            {
                MessageBox.Show("Chưa có hàng hóa nào để cập nhật thông tin!");
            }
        }

        private void btnDeleteDetailImport_CreateImport_Click(object sender, EventArgs e)
        {
            try
            {
                int idImport = Convert.ToInt32(tbxIdImport_CreateImport.Text.Trim());
                int idProduct = Convert.ToInt32(tbxIdProduct_CreateImport.Text.Trim());
                string message = "Bạn có chắc chắn muốn xóa hàng hóa này khỏi phiếu nhập không?";
                string title = "Thông báo";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    DeleteDetailImport(idImport, idProduct);
                }
            }    
            catch
            {
                MessageBox.Show("Phiếu nhập chưa có thông tin!\nVui lòng thêm chi tiết phiếu nhập trước xóa chi tiết phiếu nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btnAddImport_Click(object sender, EventArgs e)
        {
            try
            {
                int idImport = Convert.ToInt32(tbxIdImport_CreateImport.Text.Trim());
                string message = "Bạn có chắc chắn muốn thanh toán đơn hàng này không?";
                string title = "Xác nhận nhập hàng";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                if (lbTotalPriceImport_CreateImport.Text != "0 vnđ")
                {
                    DialogResult result = MessageBox.Show(message, title, buttons);
                    if (result == DialogResult.Yes)
                    {
                        CheckOutCI(idImport);
                    }
                }
                else
                {
                    MessageBox.Show("Phiếu nhập chưa có thông tin!\nVui lòng thêm chi tiết phiếu nhập trước khi nhập hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            catch
            {
                MessageBox.Show("Phiếu nhập chưa có thông tin!\nVui lòng thêm chi tiết phiếu nhập trước khi nhập hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btnCancelImport_Click(object sender, EventArgs e)
        {
            try
            {
                int idImport = Convert.ToInt32(tbxIdImport_CreateImport.Text.Trim());
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn hủy phiếu nhập này không? Thao tác này sẽ xóa tất cả các thông tin về phiếu nhập này!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
                if (result == DialogResult.Yes)
                {
                    DeleteImport(idImport);
                }
            }
            catch
            {
                MessageBox.Show("Phiếu nhập chưa có thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btnSearchImport_ManageImport_Click(object sender, EventArgs e)
        {
            try
            {
                int idImport = Convert.ToInt32(tbxSearchImport.Text.Trim());
                SearchImport(idImport);
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập giá trị hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                tbxSearchImport.Focus();
            }
        }

        private void btnDeleteImport_ManageImport_Click(object sender, EventArgs e)
        {
            int idImport = Convert.ToInt32(tbxIdImport_ManageImport.Text.Trim());
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu nhập này không? Thao tác này sẽ xóa tất cả các thông tin về phiếu nhập này!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
            if (result == DialogResult.Yes)
            {
                DeleteImport(idImport);
            }
        }

        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            
            try
            {
                string customerPhone = tbxSearchCustomer.Text;
                SearchCustomer(customerPhone);
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập giá trị hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                tbxSearchCustomer.Focus();
            }
        }

        private void btnSearchEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                int idEmployee = Convert.ToInt32(tbxSearchEmployee.Text);
                SearchEmployee(idEmployee);
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập giá trị hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                tbxSearchEmployee.Focus();
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string username = tbxUsername_Account.Text;
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đặt lại mật khẩu cho tài khoản này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                ResetPassword(username);
            }
        }

        private void btnSearch_Stock_Click(object sender, EventArgs e)
        {
            try
            {
                int idProduct = Convert.ToInt32(tbxSearchProduct.Text);
                SearchProduct(idProduct);
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập giá trị hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                tbxSearchProduct.Focus();
            }
        }

        private void lbMenuLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void lbStock_Click(object sender, EventArgs e)
        {
            LoadStock();
        }

        private void lbEmployee_Click(object sender, EventArgs e)
        {
            LoadEmployee();
        }

        private void lbCustomer_Click(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        private void lbImport_ManageImport_Click(object sender, EventArgs e)
        {
            LoadImport();
        }

        private void lbBill_ManageBill_Click(object sender, EventArgs e)
        {
            LoadBill();
        }
                
        private void btnViewReport_Click(object sender, EventArgs e)
        {
            tbxPageReport.Text = "1";
            LoadListBillByDateAndPage(dtpkFromDate.Value, dtpkToDate.Value, Convert.ToInt32(tbxPageReport.Text));
        }

        private void btnFristReportPage_Click(object sender, EventArgs e)
        {
            tbxPageReport.Text = "1";
        }

        private void btnPrevioursPage_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(tbxPageReport.Text);

            if (page > 1)
                page--;

            tbxPageReport.Text = page.ToString();
        }

        private void btnNextReportPage_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(tbxPageReport.Text);
            int sumRecord = Convert.ToInt32(BillBL.Instance.GetNumBillListByDate(dtpkFromDate.Value, dtpkToDate.Value));
            int lastPage = sumRecord / 15;

            if (sumRecord % 15 != 0)
                lastPage++;

            if (page < lastPage)
                page++;

            tbxPageReport.Text = page.ToString();
        }

        private void btnLastReportPage_Click(object sender, EventArgs e)
        {
            int sumRecord = Convert.ToInt32(BillBL.Instance.GetNumBillListByDate(dtpkFromDate.Value, dtpkToDate.Value));

            int lastPage = sumRecord / 15;

            if (sumRecord % 15 != 0)
                lastPage++;

            tbxPageReport.Text = lastPage.ToString();
        }

        private void txbPageReport_TextChanged(object sender, EventArgs e)
        {
            dgvReport.DataSource = BillBL.Instance.GetBillListByDateAndPage(dtpkFromDate.Value, dtpkToDate.Value, Convert.ToInt32(tbxPageReport.Text));
        }

        private void pnFormBill_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                int idBill = Convert.ToInt32(tbxIdBill_CreateBill.Text.Trim());
                string message = "Bạn có chắc chắn muốn rời đi không?\nNếu bạn rời đi đơn hàng hiện tại sẽ bị hủy!";
                string title = "Thông báo";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteBill(idBill);
                }
                else
                {
                    this.pnFormBill.VisibleChanged -= pnFormBill_VisibleChanged;
                    this.pnFormDashboard.Visible = false;
                    this.pnFormImport.Visible = false;
                    this.pnFormCustomer.Visible = false;
                    this.pnFormEmployee.Visible = false;
                    this.pnFormAccount.Visible = false;
                    this.pnFormStock.Visible = false;
                    this.pnFormReport.Visible = false;
                    this.pnFormBill.Visible = true;
                    this.pnFormBill.VisibleChanged += pnFormBill_VisibleChanged;
                }    
            }
            catch { }
        }

        private void pnFormImport_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                int idImport = Convert.ToInt32(tbxIdImport_CreateImport.Text.Trim());
                string message = "Bạn có chắc chắn muốn rời đi không?\nNếu bạn rời đi phiếu nhập hiện tại sẽ bị hủy!";
                string title = "Thông báo";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteImport(idImport);
                }
                else
                {
                    this.pnFormImport.VisibleChanged -= pnFormImport_VisibleChanged;
                    this.pnFormDashboard.Visible = false;
                    this.pnFormBill.Visible = false;
                    this.pnFormCustomer.Visible = false;
                    this.pnFormEmployee.Visible = false;
                    this.pnFormAccount.Visible = false;
                    this.pnFormStock.Visible = false;
                    this.pnFormReport.Visible = false;
                    this.pnFormImport.Visible = true;
                    this.pnFormImport.VisibleChanged += pnFormImport_VisibleChanged;
                }
            }
            catch { }
        }
        #endregion
    }
}
