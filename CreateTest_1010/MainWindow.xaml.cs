using CreateTest.Lib.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CreateTest_1010
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ModelEntity db = new ModelEntity();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAddSection_Click(object sender, RoutedEventArgs e)
        {
            if (btnAddSection.Content == "Add Section")
            {
                Sections sec = new Sections();
                sec.Name = tbxAddSection.Text;
                db.Sections.Add(sec);
            }
            else
            {
               
                Sections ss = (Sections)lbxListSection.SelectedItem;
                Sections sec = db.Sections.Find(ss.SectionId);
                sec.Name = tbxAddSection.Text;

            }
            tbxAddSection.Text = "";
            btnAddSection.Content = "Add Section";
            db.SaveChanges();
            lbxListSection.ItemsSource = db.Sections.ToList();
        }

        private void lbxListSection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = (ListBox) sender;
            Sections ss =(Sections) lb.SelectedItem;
            tbxAddSection.Text = ss.Name;
            btnAddSection.Content = "Edit Section";
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((TabItem)tabControl.SelectedItem).Header.ToString() == "SectionList")
            {
                lbxListSection.ItemsSource = db.Sections.ToList();
                //lvSection.ItemsSource=db.Sections.ToList();
            }
            else if(((TabItem)tabControl.SelectedItem).Header.ToString() == "AddQuestion")
            {
                cbxSelection.ItemsSource = db.Sections.ToList();
                countAnswer = 0;
            }
            else if (((TabItem)tabControl.SelectedItem).Header.ToString() == "QuestionList")
            {
                cbListSections.ItemsSource = db.Sections.ToList();
            }
        }

        private void tstSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblValueSlider.Content = ((Slider)sender).Value;
            lblValueSlider.FontSize= ((Slider)sender).Value;
            tstProgresbar.IsIndeterminate = false;
            tstProgresbar.Value= ((Slider)sender).Value;
            
        }

        private static int countAnswer=1;
        private void btnAddAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (countAnswer > 10)
            {
                MessageBox.Show("Error");
            }
            else
            {
                Label lb = new Label();
                lb.Content = countAnswer++;
                lb.Margin = new Thickness(5);
                WrapPanel wp = new WrapPanel();
                TextBox tb = new TextBox();
                tb.Margin = new Thickness(5);
                tb.Width = 400;
                CheckBox chb = new CheckBox();
                chb.Margin = new Thickness(5);
                chb.IsChecked = false;
                chb.VerticalAlignment = VerticalAlignment.Center;
                TextBox tbBall = new TextBox();
                tbBall.Margin = new Thickness(5);
                tbBall.Width = 100;
                wp.Children.Add(lb);
                wp.Children.Add(tb);
                wp.Children.Add(chb);
                wp.Children.Add(tbBall);
                spAnswerList.Children.Add(wp);
            }
        }

        private void btnSaveQuestion_Click(object sender, RoutedEventArgs e)
        {
            Questions q = new Questions();
            q.QuestionContent = tbxQuestion.Text;
            q.SectionId = ((Sections)cbxSelection.SelectedItem).SectionId;

            db.Questions.Add(q);
            db.SaveChanges();

            foreach (WrapPanel item in spAnswerList.Children)
            {
                Answers answers = new Answers();
                //TextBox tbx = (TextBox)item.Children[0];
                answers.ContentAnswer = ((TextBox)item.Children[1]).Text;
                answers.IsCorrect=(bool)((CheckBox)item.Children[2]).IsChecked;
                answers.AnswerBal=Convert.ToDouble(((TextBox)item.Children[3]).Text);
                answers.QuestionId = q.QuestionId;
                db.Answers.Add(answers);
                db.SaveChanges();

                    }
        }

        private void cbListQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int SectionId = ((Sections)cbListSections.SelectedItem).SectionId;
            foreach (Questions item in db.Questions.Where(w=>w.SectionId==SectionId))
            {
                Expander ex = new Expander();
                ex.Header = item.QuestionContent;

                WrapPanel wp = new WrapPanel();
                wp.Orientation = Orientation.Vertical;
                foreach (Answers ans in db.Answers.Where(f=>f.QuestionId==item.QuestionId))
                {
                    Label lb = new Label();
                    lb.Content = ans.ContentAnswer;
                    if (ans.IsCorrect)
                    {
                        lb.Foreground = new SolidColorBrush(Colors.Aqua);
                   }
                    else
                    {
                        lb.Foreground = new SolidColorBrush(Colors.Red);

                    }
                    wp.Children.Add(lb);
                }

                ex.Content = wp;
                wpQuestionList.Children.Add(ex);
            }
        }
    }
}
