   M S B A M L   `   `   `  ����  MercatikaApp.    (MercatikaApp.Views.ClientSelectionWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Seleccionar Cliente��5   	   $	��400��6%   $	��600��62   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5	      $
��Auto �6   .+�5   
   �� -   5        $��0��5      $��0,0,0,10q�6   $	��300��5      $
��Left=�6   ��5      �� *
SearchTerm+ �� UpdateSourceTrigger$ PropertyChanged=�5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��1��5         AutoGenerateColumns$ False��5         
IsReadOnly$
 True��6/   ��5       �� *FilteredClients+ �� SelectedItem 5      �� *SelectedClient+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6N   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 61   �� *
ClientId+6N     5      $ Empresa��6%   $ * 6V   	 66   �� *CompanyName+6V     5      $ Nombre Contacto��6%   $ * 6_   	 6>   �� *ContractName+6_   5      �� 5   
   $��2��6   
 �� Orientation$
 
Horizontal=�6"   $��Right=�6;   $��0,10,0,0q�6W   .+�5      �� -   5        $��Aceptar��5      $	��100��6'   $��5q�63   6>   �� 5      -   5        $��Cancelar��5      $	��100��6(   $��5q�64   6?   5      5       5!      