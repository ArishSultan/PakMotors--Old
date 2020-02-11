using System.Drawing;
using System.Windows.Forms;

namespace PakMotors.Components
{
    class CustomImagePanel : FlowLayoutPanel
    {
        public CustomImagePanel()
        {
            this.ControlAdded += (sender, e) =>
            {
                this.AutoScrollMinSize = new Size(this.Controls.Count * 117 + this.Controls.Count * 6, 0);
            };

            this.ControlRemoved += (sender, e) =>
            {
                this.AutoScrollMinSize = new Size(this.Controls.Count * 117 + this.Controls.Count * 6, 0);
            };
        }
    }
}
