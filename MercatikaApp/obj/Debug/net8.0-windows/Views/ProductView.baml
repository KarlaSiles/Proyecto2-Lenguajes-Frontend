   M S B A M L   `   `   `  ����G  CMercatikaApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:MercatikaApp.ViewModelMercatikaApp.ViewModel  5   	   $    MercatikaApp.Views.ProductView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3vm$clr-namespace:MercatikaApp.ViewModel     5   	     � Title$  Product management��5   	   $	��600��6$   $	��800��61   
������� � WindowStartupLocation$ CenterScreen=�5
   	    � DataContext 5      -   'MercatikaApp.ViewModel.ProductViewModel  5   
    � 	Resources 5         OrangeButton �� 
TargetType �� Property �� Value �� Triggers P� Property	 P� Value  
LabelStyle��%�  &	        &	 �    '�� �    '�� \    �� 5   
   " ��6%   .�5      ��  � �� �	#FFE67C006+   �� 5       � ��
 �����6+   �� 5       � ��$ 0q�60   �� 5       � ��$
 10,5q�6(   �� 5       � ��$ Medium"�6+   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� �	#FFD269006,    5      P� 5       �@J�IsMouseOver$
	 True��61   . �5      ��  � �� �	#FFD2690063   5      5      �� 5    
   " ��6#   .�5!      ��  � ��$ 14(�6)   �� 5"       � ��$	 5,0q�6'   �� 5#       � �� �	#FF1F1F1F6+   �� 5$       � ��$ SemiBold"�6+   �� 5%       � ��$ Center=�62   5&      �� 5(   
   " ��6   .�5)      ��  � ��$ 5q�6'   �� 5*       � ��$ 5q�6(   �� 5+       � ��$ 13(�6)   �� 5,       � �� �	#FFCCCCCC6,   5-      �� 5/   
   " ��6   .�50      ��  � ��$ 5q�6'   �� 51       � ��$ 5q�6(   52      53      .��55      ]� .+�57   
   �� $	��Top=�6   
�������.+�58      � $
��10,5q�6   $��70��6!   
 � ColumnDefinitions
 59      �� 5:      $
��Auto �6'   �� 5;      .+�5>      �� $��Resources/LogoMK.png��6   $��60��5?      $��Center=�5@      $
��Left=�5A      $��0��5B      $
��10,0q�5C      5D      �� 5F      $��4��6   ���	#FFE67C00$��0,0,0,10q�67   5G      � 5J   
   $��20q�6    � RowDefinitions 5K      �� 5L      $
��Auto �6    �� 5M      $��* �6    �� 5N      $
��Auto �6    .+�5R      ��  �� Orientation$ 
Horizontal=�6   $��0��62   $��0 0 0 10q�6?   .+�5S      �� $	��300��6   ��5T      �� *
SearchTerm+ �� UpdateSourceTrigger$ PropertyChanged=��� 5U      $��Search��6   $��80��5V      #��[  5X      ��5W      �� *SearchCommand+5X      5Y      &   System.Windows.Controls.DataGrid  5\      $��1��6      AutoGenerateColumns$ False��5_         CanUserAddRows$ False��5`      $	��300��5a      $��0 0 0 10q�5b      ��5]      �� *
Products+ �� SelectedItem 5^      �� *SelectedProduct+   Columns 5c      0  *System.Windows.Controls.DataGridTextColumn  5d      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 5f   )   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5e   )   �� *	ProductId+5f   )     5g      $
 Name��6)   $	 200 5i   )    5h   )   �� *ProductName+5i   )     5j      $ Price��6)   $	 100 5l   )    5k   )   �� *Price+5l   )     5m      $ Category��6)   $	 150 5o   )    5n   )   �� *CategoryCode.Description+5o   )   5q      �� 5t      $��2��6   $ Vertical=�6&   .+�5u      �� $ 
Horizontal=�6   $��0 0 0 10q�66   .+�5v      �� $��Name:��6    $��60��6-   #��[ 68   �� 5w      $	��200��6   ��5x      �� *SelectedProduct.ProductName+$ PropertyChanged=��� 5z      $��Price:��6    $��60��6.   #��[ 69   �� 5{      $	��100��6   ��5|      �� *SelectedProduct.Price+$ PropertyChanged=� �� TargetNullValue$ 0���� 5~      $��Category ID:��6    $��80��64   #��[ 6?   �� 5      $��50��6   ��5�      �� *+)SelectedProduct.CategoryCode.CategoryCode+$ PropertyChanged=�$ 0��5�      �� 5�      $ 
Horizontal=�6   $
��Left=�66   .+�5�      �� -   5        $��Insert��5�      $	��100��5�      #��[  5�      �� 5�      $��Update��6   $	��100��5�      #��[  5�      ��5�      �� *UpdateCommand+5�      �� 5�      $��Delete��6   $	��100��5�      #��[  5�      ��5�      �� *DeleteCommand+5�      5�      5�      5�      5�      5�      