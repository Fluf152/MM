   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WpfApp1.PagesWpfApp1.Pages  5          WpfApp1.Pages.ThirdPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:WpfApp1.Pages     5        N� Title$  	ThirdPage��5	      .��5      � 
������� � ColumnDefinitions 5   
   �� 5      $	��150 �6   �� 5      .+�5   
   �� $��10q�6   .+�5      �� -   5        $'��!Рандомная матрица��5      $	��0,5q�5      �� 5      -   5        $��К триуголькой��5      $	��0,5q�5      �� 5      -   5        $!��К диогональной��5      $	��0,5q�5      5      � 5   
   $��1��6    � RowDefinitions 5      �� 5      �� 5       �� 5!      .+�5$      �� $��0��6   .+�5%      �� $.��(Оригинальная матрица:��6   $
��Bold"�5&      $
��10,5q�5'      &   System.Windows.Controls.DataGrid  5(      -   5          Name$ OriginalMatrixGrid��5(      $��10q�5)         AutoGenerateColumns .    CanUserAddRows .     HeadersVisibility$	 Row=�5,      $
��Left=�5-      5.      �� 50      $��1��6   .+�51      �� $,��&Триугольная матрица:��6   $
��Bold"�52      $
��10,5q�53        54      -   5        $ TriangularMatrixGrid��54      $��10q�55       .  .  $	 Row=�58      $
��Left=�59      5:      �� 5<      $��2��6   .+�5=      �� $.��(Диогональная матрица:��6   $
��Bold"�5>      $
��10,5q�5?        5@      -   5        $ DiagonalMatrixGrid��5@      $��10q�5A       .  .  $	 Row=�5D      $
��Left=�5E      5F      5G      5H      5I      