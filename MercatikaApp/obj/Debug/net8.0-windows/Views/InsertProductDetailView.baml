   M S B A M L   `   `   `  ����G  CMercatikaApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:MercatikaApp.ViewModelMercatikaApp.ViewModel  5   	   0    *MercatikaApp.Views.InsertProductDetailView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   3vm$clr-namespace:MercatikaApp.ViewModel     5   	     � Title$%  Gestión de Detalle de Producto��5   	   $	��500��60   $	��550��6=    � WindowStartupLocation$ CenterScreen=�5   	   
������� � DataContext 5	      3   -MercatikaApp.ViewModel.ProductDetailViewModel  5
   
    � 	Resources 5         OrangeButton �� 
TargetType �� Property �� Value �� Triggers P� Property	 P� Value  
LabelStyle��%�  &	        &	 �    '�� ~    '�� ?    �� 5   
   " ��6%   .�5      ��  � �� �	#FFE67C006+   �� 5       � ��
 �����6+   �� 5       � ��$ 0q�60   �� 5       � ��$
 10,5q�6(   �� 5       � ��$ Medium"�6+   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� �	#FFD269006,    5      P� 5       �@J�IsMouseOver$
	 True��61   . �5      ��  � �� �	#FFD2690063   5      5      �� 5   
   " ��6#   .�5       ��  � ��$ 14(�6)   �� 5!       � ��$ 0,5,0,2q�6'   �� 5"       � �� �	#FF1F1F1F6+   �� 5#       � ��$ SemiBold"�6+   5$      �� 5'   
   " ��6   .�5(      ��  � ��$ 0,0,0,10q�6'   �� 5)       � ��$ 5q�6(   �� 5*       � ��$ 13(�6)   �� 5+       � �� �	#FFCCCCCC6,   5,      �� 5/   
   " ��6   .�50      ��  � ��$ 0,0,10,0q�6'   �� 51       � ��$ 5q�6(   52      53      .��55      � $��20q�6   
 � RowDefinitions
 56   
   �� 57      $
��Auto �6   �� 58      $��* �6   �� 59      $
��Auto �6   .+�5=   
   ��  �� Orientation$ 
Horizontal=�6   $��0 0 0 10q�6.   $��0��6@   .+�5>      �� $	��300��6   $��0 0 10 0q�5@      ��5?      �� *
SearchTerm+ �� UpdateSourceTrigger$ PropertyChanged=�5@      �� 5A      $��Buscar��6   $��80��5C      #��[  5D      ��5B      �� *SearchCommand+5D      5E      &   System.Windows.Controls.DataGrid  5H   
   $��1��6      AutoGenerateColumns$ False��5K      $	��200��5L      $��0 0 0 10q�5M         CanUserAddRows$ False��5N      ��5I      �� *
Products+ �� SelectedItem 5J      �� *SelectedProduct+   Columns 5O      0  *System.Windows.Controls.DataGridTextColumn  5P      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6O   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *	ProductId+6O     5Q      $ Nombre��6%   $	 150 6U    65   �� *ProductName+6U     5R      $ Precio��6%   $	 100 6O    65   �� *Price+6O     5S      $ 
Categoría��6%   $	 150 6e    68   �� *CategoryCode.Description+6e   5U      �� 5X   
   $��2��6   $ Vertical=�6"   .+�5Y      �� $��Código único:��6   #��[ 6-   �� 5Z      ��6   �� *!ProductDetail.UniqueProductCode+$ PropertyChanged=��� 5\      $��Cantidad en stock:��6   $��0,10,0,0q�62   #��[ 6D   �� 5]      ��6   �� *ProductDetail.StockAmount+$ PropertyChanged=��� 5_      $��Talla (opcional):��6   $��0,10,0,0q�61   #��[ 6C   �� 5`      ��6   �� *ProductDetail.Size+$ PropertyChanged=��� 5c      $ 
Horizontal=�6   $��0,20,0,0q�62   .+�5d      �� $��Insertar��6   $	��100��5f      #��[  5g      ��5e      �� *InsertCommand+5g      �� 5h      $��
Actualizar��6   $	��100��5j      #��[  5k      ��5i      �� *UpdateCommand+5k      5l      5m      5n      5o      