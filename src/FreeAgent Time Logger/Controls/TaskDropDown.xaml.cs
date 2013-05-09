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

namespace FreeAgent_Time_Logger.Controls
{
    public partial class TaskDropDown : UserControl
    {
        public TaskDropDown()
        {
            InitializeComponent();
        }

        public void UpdateTaskList()
        {
            TimeTracking.Api.Models.Project
            //ddlTasks.Items.Add();
        }
    }
}
