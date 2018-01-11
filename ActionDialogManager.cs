using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExplorer
{
    class ActionDialogManager
    {
        static ActionDialog previousActionDialog;
        public static void showDialog(ActionDialog actionDialog)
        {
            if(previousActionDialog != null)
            {
                previousActionDialog.Hide();
                
            }
            previousActionDialog = actionDialog;
            actionDialog.Show();
        }

        public static void hideAll()
        {
            if (previousActionDialog != null)
            {
                previousActionDialog.Hide();

            }
        }
    }
}
