   M S B A M L   `   `   `  ����E  AClassePage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ClassePage
ClassePage  5   	   "    ClassePage.PageToShowClasses   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:ClassePage     5   	   -   5          � Title$  Mon cahier de notes��5   	   $	��450��6%   $	��800��65   $	��450��6D   $	��800��6T   .��5	      �  � RowDefinitions 5
   
   �� 5      $��0.25* �6   �� 5      $��* �6   �� 5      $��0.25* �6   .+�5   
   ]� .+�5      o� .@�5      m� $��_Mes élèves��6   .@�5      m� -   5        $��_Afficher la page��5      66   m� 5      -   5        $��_Exit��5      6*   5      m� 5      $��_Mes interrogations��6   .@�5      m� -   5        $��_Afficher la page��5      6:   m� 5      -   5        $��_Exit��5      6.   5      5      5      �� 5   
   $��0��6   $��24(�6   $��Center=�6+   $��Center=�6H   .��5      �� �� 5       $
��Bold"�6    �� TextDecorations$ 	Underline{�6(   .�6D   Affichage de vos classes :6`   5!      5"      &   System.Windows.Controls.DataGrid  5$   
   -   5          Name$ dataGrid��5$       $��1��6      AutoGenerateColumns$ False��6|      CanUserAddRows$ false��6�      CanUserDeleteRows$ False��6�    �� SelectedItem 62   �� *CurrentClass+��6X   �� *ListClasses+   Columns 5%      0  *System.Windows.Controls.DataGridTextColumn  5&      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Nom de la classe��6%   
   
IsReadOnly$

 True��6]   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6?   �� *	ClassName+6]     5'      $	 Nombre d'étudiants��6%   $

 True��6`    6A   �� *
NbStudents+6`   5)      �� 5+   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $��Center=�6X   $��	0,10,0,30q�6s   .+�5,      �� -   5        $��Ajouter une classe��5,      $	��100��62   $��5q�6>   6I   �� 5-      -   5        $��Modifier une classe��5-      $	��110��63   $��5q�6?   6J   �� 5.      -	   5        $��Supprimer une classe��5.      $	��115��64   $��5q�6@   6K   �� 5/      -
   5        $*��$Ajouter les élèves à votre classe��5/      $	��170��6A   $��5q�6M   6X   �� 50      -   5        $��Ajouter une interrogation��50      $	��150��69   $��5q�6E   6P   51      52      53      