   M S B A M L   `   `   `  ����  MercatikaApp/    )MercatikaApp.Views.ProductSelectionWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Seleccionar Producto��5   	   $	��400��6&   $	��600��63   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5	      $
��Auto �6   .+�5   
   �� -   5        $��0��5      $��0,0,0,10q�6   $	��300��5      $
��Left=�6   ��5      �� *
SearchTerm+ �� UpdateSourceTrigger$ PropertyChanged=�5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��1��5         AutoGenerateColumns$ False��5         
IsReadOnly$
 True��6/   ��5       �� *
Products+ �� SelectedItem 5      �� *SelectedProduct+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Producto��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *ProductName+  5      $ Precio��6%    65   �� *Price+5      �� 5   
   $��2��6   	 �� Orientation$	 
Horizontal=�6"   $��Right=�6;   $��0,10,0,0q�6W   .+�5      �� $��	Cantidad:��6   $��Center=�6)   $��0,0,10,0q�6D   �� 5      $��60��6   $��0,0,20,0q�6C   ��6!   �� *SelectedQuantity+6C   �� 5      -   5        $��Aceptar��5      $	��100��6'   $��5q�63   6>   �� 5       -   5        $��Cancelar��5       $	��100��6(   $��5q�64   6?   5!      5$      5%      