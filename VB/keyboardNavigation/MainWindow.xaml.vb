' Developer Express Code Central Example:
' DXGrid Keyboard Navigation integration with AutoCompleteBox
' 
' Keyboard navigation support with custom editors in the DXGrid may be
' accomplished by setting a custom attached behavior -- available from the
' System.Windows.Interactivity library -- on the TableView to assign the a handler
' to the GridViewBase.EditorShowing
' (http://documentation.devexpress.com/#WPF/DevExpressXpfGridGridViewBase_ShowingEditortopic)
' event. There, you may handle the focus logic for the custom control.
' In the
' following example, we've handled the focus logic for the standard Slider control
' and the custom AutoCompleteBox from the WPF ToolKit -- WPF Toolkit - February
' 2010 Release (http://wpf.codeplex.com/releases/view/40535).
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E20032

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Mvvm.UI.Interactivity

Namespace attachedBehaviorPolicy
    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Partial Public Class MainWindow
        Inherits Window

        Public Sub New()
            InitializeComponent()
            gridControl1.ItemsSource = TestDataItems.Create()
        End Sub
        Private Sub FocusEditor_AutoComplete(ByVal sender As Object, ByVal e As FocusEditorEventArgs)
            Dim a As AutoCompleteBox = CType(e.Editor, AutoCompleteBox)
            Dim mainEditor As TextBox = TryCast(a.Template.FindName("Text", a), TextBox)
            mainEditor.Focus()
        End Sub
        Private Sub FocusEditor_Slider(ByVal sender As Object, ByVal e As FocusEditorEventArgs)
            e.Editor.Focus()
        End Sub
    End Class
    Public Class TestDataItems
        Inherits List(Of TestDataItem)

        Public Shared Function Create() As TestDataItems
            Dim t As New TestDataItems()
            For i As Integer = 0 To 49
                Dim d As TestDataItem = New TestDataItem With {.ID = i, .Value = (i Mod 5 / 4).ToString() & (i Mod 3 / 2).ToString() & (i Mod 2).ToString(), .Value2= (i Mod 5 / 4) * 5 + (i Mod 3 / 2) * 3 + (i Mod 2) * 2}
                t.Add(d)
                If TestDataItem.Domain Is Nothing Then
                    TestDataItem.Domain = New List(Of String)()
                End If
                If Not TestDataItem.Domain.Contains(d.Value) Then
                    TestDataItem.Domain.Add(d.Value)
                End If
            Next i
            Return t
        End Function
    End Class
    Public Class TestDataItem
        Public Shared Property Domain() As List(Of String)
        Public Property Value() As String
        Public Property Value2() As Double
        Public Property ID() As Integer
    End Class
    Friend Class KeyboardNavigationBehavior
        Inherits Behavior(Of TableView)

        Public Shared ReadOnly ColumnNameProperty As DependencyProperty = DependencyProperty.Register("ColumnName", GetType(String), GetType(KeyboardNavigationBehavior), New PropertyMetadata(String.Empty))
        Public Property ColumnName() As String
            Get
                Return CStr(GetValue(ColumnNameProperty))
            End Get
            Set(ByVal value As String)
                SetValue(ColumnNameProperty, value)
            End Set
        End Property
        Public Event FocusEditor As FocusEditorEventHandler
        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.ShowingEditor, AddressOf OnShowingEditor
        End Sub
        Protected Overrides Sub OnDetaching()
            RemoveHandler AssociatedObject.ShowingEditor, AddressOf OnShowingEditor
            MyBase.OnDetaching()
        End Sub
        Public Sub OnShowingEditor(ByVal o As Object, ByVal e As ShowingEditorEventArgs)
            If e.Column.Name <> ColumnName Then
                Return
            End If
            Dim cp = AssociatedObject.GetCellElementByRowHandleAndColumn(e.RowHandle, e.Column)
            Dim editor As FrameworkElement = LayoutHelper.FindElementByName(cp, "PART_Editor")
            RaiseEvent FocusEditor(Me, New FocusEditorEventArgs(editor))
        End Sub
    End Class
    Public Delegate Sub FocusEditorEventHandler(ByVal s As Object, ByVal e As FocusEditorEventArgs)
    Public Class FocusEditorEventArgs
        Inherits EventArgs

        Private privateEditor As FrameworkElement
        Public Property Editor() As FrameworkElement
            Get
                Return privateEditor
            End Get
            Private Set(ByVal value As FrameworkElement)
                privateEditor = value
            End Set
        End Property
        Public Sub New(ByVal editor As FrameworkElement)
            Me.Editor = editor
        End Sub
    End Class
End Namespace
