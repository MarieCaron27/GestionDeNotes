   M S B A M L   `   `   `  ����D  @QuizzPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:QuizzPage	QuizzPage  5   	   !    QuizzPage.PageToShowQuizzes   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:QuizzPage     5   	   -   5          � Title$  
MainWindow��5   	   $	��450��6   $	��450��6,   $	��960��6<   $	��960��6K   .��5	      �  � RowDefinitions 5
   
   �� 5      $��0.25* �6   �� 5      $��* �6   �� 5      $��0.25* �6   .+�5   
   �� $��0��6   $��24(�6   $��Center=�6+   $��Center=�6H   .��5      �� �� 5      $
��Bold"�6    �� TextDecorations$ 	Underline{�6(   .�6D   #!Affichage de vos interrogations :6g   5      5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGrid��5       $��1��6      AutoGenerateColumns$ False��6|      CanUserAddRows$ false��6�      CanUserDeleteRows$ False��6�    �� SelectedItem 62   �� *CurrentQuizz+��6X   �� *ListQuizzes+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Id��6%   
   
IsReadOnly$

 True��6M   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *	QuizzId+6M     5      $"	 Intitulé de l'interrogation��6%   $

 True��6i    6J   �� *
QuizzTitle+6i     5      $	 Date de l'interrogation��6%   $

 True��6}    6F   �� *	QuizzDate+ �� StringFormat$ 
dd/MM/yyyy��6}     5      $=	 7Classe dans laquelle l'interrogation a été réalisée��6%   $

 True��6�    6b   �� *QuizzClass.ClassName+6�     5      $:	 4Cours pour lequel l'interrogation a été réalisée��6%   $

 True��6    6_   �� *QuizzCourse+6     5      $	 Total de l'interrogation��6%   $

 True��6f    6G   �� *
QuizzTotal+6f   5      �� 5!   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $��Center=�6X   $��	0,10,0,30q�6s   .+�5"      �� -   5        $ ��Modifier une interrogation��5"      $	��150��6:   $��5q�6F   6Q   �� 5#      -   5        $!��Supprimer une interrogation��5#      $	��155��6;   $��5q�6G   6R   �� 5$      -   5        $/��)Ajouter les résultats de l'interrogation��5$      $	��210��6H   $��5q�6T   6_   5%      5'      5(      