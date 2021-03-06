using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SQLite;
using System.Text.RegularExpressions;

namespace FinalProject
{


    [Activity(Label = "Signup")]
    public class ActivitySignup : Activity
    {
        //ROLES***************************************************


        private RadioButton radioBtnAdmin;
        private RadioButton radioBtnUser;


        private Button buttonSignup;
        private EditText editTextEmail;
        private EditText editTextPass;
        private EditText editTextConfirmPass;
        Regex validPass = new Regex(@"^(?=.*[0-9])" // ít nhất 1 số
                       + "(?=.*[a-z])(?=.*[A-Z])" //ít nhất 1 letter hoa, thường
                       + "(?=.*[@#$%^&+=])" //ít nhất 1 kí tự đặc biệt
                       + "(?=\\S+$).{8,20}$"); //Không chứa khoảng trắng min8, max20

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.sign_up);
            //Tạo nút back Home chưa thành công
            //ActionBar.SetDisplayShowHomeEnabled(true);
            //ActionBar.SetDisplayHomeAsUpEnabled(true);

            editTextEmail = FindViewById<EditText>(Resource.Id.editTextEmail);
            editTextPass = FindViewById<EditText>(Resource.Id.editTextPass);
            editTextConfirmPass = FindViewById<EditText>(Resource.Id.editTextConfirmPass);

            buttonSignup = FindViewById<Button>(Resource.Id.buttonSignup);
            buttonSignup.Click += ButtonSignup_Click;


            //ROLES***************************************************

            radioBtnAdmin = FindViewById<RadioButton>(Resource.Id.radio_admin);
            radioBtnUser = FindViewById<RadioButton>(Resource.Id.radio_user);
            radioBtnAdmin.Click += RadioButton_Click;
            radioBtnUser.Click += RadioButton_Click;
        }

        //Chưa xử lý ràng buộc dữ liệu nhập vào
        private  void ButtonSignup_Click(object sender, EventArgs e)
        {
            string inputEmail = editTextEmail.Text.ToString();
            string inputPass = editTextPass.Text.ToString();
            var emailValidate = isValidEmail(inputEmail);
            radioBtnAdmin = FindViewById<RadioButton>(Resource.Id.radio_admin);
            radioBtnUser = FindViewById<RadioButton>(Resource.Id.radio_user);
            try
            {
                //Password equal confirm_pass ---editTextEmail.Text !="" && editTextPass.Text !="" && editTextPass.Text == editTextConfirmPass.Text
                if ((string.IsNullOrWhiteSpace(editTextEmail.Text))||(string.IsNullOrWhiteSpace(editTextPass.Text))||
                    (string.IsNullOrEmpty(editTextEmail.Text))||(string.IsNullOrEmpty(editTextPass.Text)))
                {
                    Toast.MakeText(this, "Information is invalid", ToastLength.Short).Show();
                    
                }
                //Check email input: using built-in method  OR Regex custom
                else if (!emailValidate)
                {
                    Toast.MakeText(this, " Invalid email", ToastLength.Short).Show();

                }
                else if(isValidPass(inputPass)==false)
                {
                    Toast.MakeText(this, "Password length must have digit, lower letter, upper leter and special character", ToastLength.Short).Show();

                }
                else if(!string.Equals(editTextPass.Text, editTextConfirmPass.Text))
                {
                    //editTextPass.Text = string.Empty;
                    editTextConfirmPass.Text = string.Empty;
                    Toast.MakeText(this, "Password is not match", ToastLength.Short).Show();
                }    
                else
                {
                    string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
                    var db = new SQLiteConnection(dbPath);
                    db.CreateTable<LoginTable>();
                    LoginTable tbl = new LoginTable();
                    tbl.email = editTextEmail.Text;
                    tbl.password = editTextPass.Text;

                    //ROLES***************************************************

                    //Add roles
                    if (radioBtnAdmin.Checked)
                        tbl.role = "ADMIN";
                    else if (radioBtnUser.Checked)
                        tbl.role = "USER";
                   
                      
                    //TODO WHAT??    Xứ lý ràng buộc unique email 
                    //var SQL = db.Query<LoginTable>("SELECT * from users where email=editTextEmail.Text"); failed

                    var data = db.Table<LoginTable>(); //call table
                    var uniqueEmail = data.Where(x => x.email == editTextEmail.Text).FirstOrDefault(); //return first element
                    if (uniqueEmail == null)
                    {
                        db.Insert(tbl);
                        Toast.MakeText(this, "Sign up successfully", ToastLength.Short).Show();
                        StartActivity(typeof(MainActivity));
                    }
                    else
                    {
                        Toast.MakeText(this, "Email already exists!", ToastLength.Short).Show();
                    }

                }

            }
            catch(Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        //Email regex built-in
        public bool isValidEmail(string email)
        {
            return Android.Util.Patterns.EmailAddress.Matcher(email).Matches();
        }
        public bool isValidPass(string pass)
        {
            return validPass.IsMatch(pass);
        }

        //ROLES***************************************************

        public void RadioButton_Click(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            Toast.MakeText(this, "Your role is  "+rb.Text , ToastLength.Short).Show();
        }

        
            
    }
}