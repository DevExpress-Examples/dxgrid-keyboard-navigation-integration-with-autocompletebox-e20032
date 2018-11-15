<!-- default file list -->
*Files to look at*:

* [MainWindow.xaml](./CS/keyboardNavigation/MainWindow.xaml) (VB: [MainWindow.xaml.vb](./VB/keyboardNavigation/MainWindow.xaml.vb))
* [MainWindow.xaml.cs](./CS/keyboardNavigation/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/keyboardNavigation/MainWindow.xaml.vb))
<!-- default file list end -->
# DXGrid Keyboard Navigation integration with AutoCompleteBox


<p>Keyboard navigation support with custom editors in the DXGrid may be accomplished by setting a custom attached behavior -- available from the System.Windows.Interactivity library -- on the TableView to assign the a handler to the <a href="http://documentation.devexpress.com/#WPF/DevExpressXpfGridGridViewBase_ShowingEditortopic">GridViewBase.EditorShowing</a> event. There, you may handle the focus logic for the custom control.</p><p>In the following example, we've handled the focus logic for the standard Slider control and the custom AutoCompleteBox from the WPF ToolKit -- <a href="http://wpf.codeplex.com/releases/view/40535">WPF Toolkit - February 2010 Release</a>.</p>

<br/>


