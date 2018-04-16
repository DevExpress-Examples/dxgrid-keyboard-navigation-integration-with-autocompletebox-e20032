// Developer Express Code Central Example:
// DXGrid Keyboard Navigation integration with AutoCompleteBox
// 
// Keyboard navigation support with custom editors in the DXGrid may be
// accomplished by setting a custom attached behavior -- available from the
// System.Windows.Interactivity library -- on the TableView to assign the a handler
// to the GridViewBase.EditorShowing
// (http://documentation.devexpress.com/#WPF/DevExpressXpfGridGridViewBase_ShowingEditortopic)
// event. There, you may handle the focus logic for the custom control.
// In the
// following example, we've handled the focus logic for the standard Slider control
// and the custom AutoCompleteBox from the WPF ToolKit -- WPF Toolkit - February
// 2010 Release (http://wpf.codeplex.com/releases/view/40535).
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E20032

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Core.Native;
using DevExpress.Mvvm.UI.Interactivity;

namespace attachedBehaviorPolicy {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            gridControl1.ItemsSource = TestDataItems.Create();
        }
        private void FocusEditor_AutoComplete(object sender, FocusEditorEventArgs e) {
            AutoCompleteBox a = (AutoCompleteBox)e.Editor;
            TextBox mainEditor = a.Template.FindName("Text", a) as TextBox;
            mainEditor.Focus();
        }
        private void FocusEditor_Slider(object sender, FocusEditorEventArgs e) {
            e.Editor.Focus();
        }
    }
    public class TestDataItems : List<TestDataItem> {
        public static TestDataItems Create () {
            TestDataItems t = new TestDataItems();
            for (int i = 0; i < 50; i++) {
                TestDataItem d = new TestDataItem {
                    ID = i, 
                    Value = (i % 5 / 4).ToString() + (i % 3 / 2).ToString() + (i % 2).ToString(), Value2= (i % 5 / 4) * 5 + (i % 3 / 2) * 3 + (i % 2) * 2};
                t.Add(d);
                if (TestDataItem.Domain == null) TestDataItem.Domain = new List<string>();
                if (!TestDataItem.Domain.Contains(d.Value)) TestDataItem.Domain.Add(d.Value);
            }
            return t;
        }
    }
    public class TestDataItem {
        public static List<string> Domain { get; set; }
        public string Value { get; set; }
        public double Value2 { get; set; }
        public int ID { get; set; }
    }
    internal class KeyboardNavigationBehavior : Behavior<TableView> {
        public static readonly DependencyProperty ColumnNameProperty =
            DependencyProperty.Register("ColumnName", typeof(string), typeof(KeyboardNavigationBehavior), new PropertyMetadata(string.Empty));
        public string ColumnName {
            get { return (string)GetValue(ColumnNameProperty); }
            set { SetValue(ColumnNameProperty, value); }
        }
        public event FocusEditorEventHandler FocusEditor;
        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.ShowingEditor += OnShowingEditor;
        }
        protected override void OnDetaching() {
            AssociatedObject.ShowingEditor -= OnShowingEditor;
            base.OnDetaching();
        }
        public void OnShowingEditor(object o, ShowingEditorEventArgs e) {
            if (e.Column.Name != ColumnName) return;
            var cp = AssociatedObject.GetCellElementByRowHandleAndColumn(e.RowHandle, e.Column);
            FrameworkElement editor = LayoutHelper.FindElementByName(cp, "PART_Editor");
            if (FocusEditor != null) FocusEditor(this, new FocusEditorEventArgs(editor));
        }
    }
    public delegate void FocusEditorEventHandler(object s, FocusEditorEventArgs e);
    public class FocusEditorEventArgs : EventArgs {
        public FrameworkElement Editor { get; private set; }
        public FocusEditorEventArgs(FrameworkElement editor) {
            Editor = editor;
        }
    }
}
