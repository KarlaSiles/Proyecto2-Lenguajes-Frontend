   M S B A M L   `   `   `  ����  MercatikaApp&     MercatikaApp.Views.ClientsUcView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      �   � RowDefinitions  5	   
   �� 5
      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   ���#3498DB$��5q�63   ._�5      ��  �� Orientation$ 
Horizontal=�6   $��10q�62   .+�5      �� $	��200��6   $��5q�5      $��Center=�6$   ��5   &   �� *
SearchTerm+ �� UpdateSourceTrigger$ PropertyChanged=�5   $   �� 5      $��Search��6   $��5q�6*   ��65   �� *SearchCommand+�� 5      $��Clear��6   $��5q�6)   ��64   �� *ClearSearchCommand+�� 5      $��
Add client��6   $��5q�6.   ��69   �� *OpenAddClientWindowCommand+�� 5      $��Edit client��6   $��5q�6/   ��6:   �� *OpenEditClientWindowCommand+{�5      �� *IsClientSelected+�� 5      $��Delete client��6   $��5q�61   ��6<   �� *DeleteClientCommand+{�5      �� *IsClientSelected+5      5      &   System.Windows.Controls.DataGrid  5   
   $��1��6      AutoGenerateColumns$ False��5!         
IsReadOnly$
 True��5"      $��5q�5#         SelectionMode$ Single=�5$         AlternatingRowBackground �#F5F5F5   GridLinesVisibility$
 None=�5&         RowBackground
 �����$��12(�5(      	   HeadersVisibility$	 Column=�5)      ��5      �� *FilteredClients+
 �� SelectedItem
 5       �� *SelectedClient+   Columns 5+      0  *System.Windows.Controls.DataGridTextColumn  5,      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6N   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *
ClientId+6N     5-      $ Company��6%   $ * 6V    66   �� *CompanyName+6V     5.      $ Contact��6%   $ * 6W    66   �� *ContractName+6W     5/      $ Lastname contact��6%   $ * 6d    6?   �� *ContractLastname+6d     50      $ Position contact��6%   $ * 6d    6?   �� *ContractPosition+6d     51      $ Phone number��6%   $ * 6U    6;   �� *Phone+6U     52      $	 Fax��6%   $ * 6P    62   �� *	FaxNumber+6P     53      $ Address��6%   $ * 6R    66   �� *	Address+6R     54      $
 City��6%   $ * 6L    63   �� *City+6L     55      $ Province��6%   $ * 6T    67   �� *
Province+6T     56      $ Zip code��6%   $ * 6S    67   �� *	ZipCode+6S     57      $ Country��6%   $ * 6R    66   �� *	Country+6R      	CellStyle 5:      �� 5;       �� 
TargetType*  $System.Windows.Controls.DataGridCell"  6   .�5<      ��  �� Property � �� �� Value$ 0q�68   �� 5=       �@�FocusVisualStyle 69   Z� �� 5>       � �� 5?      �� 5@       �� 
TargetType"  6.   .R�5A   "   �� #��z��6)   ._�5B   &   �� $	��8,5q�67   $��Center=�5C   5   $
��Left=�5D   5   5E   #   5F      5H      5I         ColumnHeaderStyle 5L      �� 5M      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5N      ��  � �� �#082c5463   �� 5O       � ��
 �����63   �� 5P       � ��$ SemiBold"�63   �� 5Q       � ��$
 Left=�6C   �� 5R       � ��$	 8,5q�60   5S      5U      5V      5W      