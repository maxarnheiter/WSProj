   M S B A M L   `   `   `  ����A  =WSProj, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WSProjWSProj  5   	       WSProj.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:WSProj     5   	   -   5          � Title$  
MainWindow��5   	   $��458.403��6   $	��798��6-   .��5	      � .+�5
   
   �� -   5         ��Name$ 
tabControl��5
      $
��Left=�6)   $	��428��6D   $	��Top=�6Q   $	��790��6i   .@�5      �� $��Connect��6   .��5      � ���	#FFE5E5E5.+�5      �� -   5         ��Name$ Step1GroupBox��5      $��Step 1��66   $
��Left=�6F   $��	10,10,0,0q�6a   $	��Top=�6t   $	��376��6�   $	��219��6�   .��5      � $
��Left=�6   $	��366��6:   $	��Top=�6G   $	��209��6_   $��
0,0,-2,-12q�6k    � ColumnDefinitions 5      �� 5   "   $��0* �63   �� 5   "   $��0* �63   �� 5   "   .+�5      �� -   5         ��Name$ BaudRateComboBox��5   '   $
��Left=�6A   $��
10,216,0,0q�6\   $	��Top=�6p   $	��189��6�   $��2��6�   $��22��6�   �� 5      -   5         ��Name$ BaudRateLabel��5   $   $��
Baud Rate:��6;   $
��Left=�6P   $��
10,195,0,0q�6k   $	��Top=�6   $	��189��6�   $��2��6�   $��26��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�    J� IsHitTestVisible .  z�5   "   V� 5   &   .~�5   *   �� �� 5   *   T� 5   *   5   '   5      �� 5      -   5        $ DataBitsComboBox��5   '   $
��Left=�6A   $��
10,163,0,0q�6\   $	��Top=�6p   $	��189��6�   $��2��6�   $��22��6�   �� 5      -   5        $ DataBitsLabel��5   $   $��
Data Bits:��6;   $
��Left=�6P   $��
10,141,0,0q�6k   $	��Top=�6   $	��189��6�   $��2��6�   $��26��6�    .  6�   �� 5       -   5        $ StopBitsComboBox��5    '   $
��Left=�6A   $��
10,322,0,0q�6\   $	��Top=�6p   $	��189��6�   $��2��6�   $��22��6�   �� 5!      -	   5        $ StopBitsLabel��5!   $   $��
Stop Bits:��6;   $
��Left=�6P   $��
10,300,0,0q�6k   $	��Top=�6   $	��189��6�   $��2��6�   $��26��6�    .  6�   �� 5"      -
   5        $ ParityComboBox��5"   '   $
��Left=�6?   $��
10,269,0,0q�6Z   $	��Top=�6n   $	��189��6�   $��2��6�   $��22��6�   �� 5#      -   5        $ ParityLabel��5#   $   $��Parity Bits:��69   $
��Left=�6P   $��
10,247,0,0q�6k   $	��Top=�6   $	��189��6�   $��2��6�   $��26��6�    .  6�   �� 5$      -   5        $ ComPortLabel��5$   $   $��
Com Ports:��6:   $
��Left=�6O   $��	10,62,0,0q�6j   $	��Top=�6}   $	��189��6�   $��2��6�   $��26��6�    .  6�   �� 5%      -   5        $ ComPortComboBox��5%   '   $
��Left=�6@   $��	10,83,0,0q�6[   $	��Top=�6n   $	��189��6�   $��2��6�   $��22��6�   �� 5&      -   5         ��Name$# AdvancedCommunicationCheckbox��5&   '   $��Advanced Options��6N   $
��Left=�6i   $��
13,120,0,0q�6�   $	��Top=�6�   $	��178��6�   $��3��6�   6   �� 5'      -   5        	 ��Name$	 Step1TextBlock��5'   (   $
��Left=�6@   $��	10,10,0,0q�6[   $
��Wrap=�6n   $G��ASelect a COM Port. You may choose to adjust the advanced options.��6�   $	��Top=�6�   $��47��6�   $	��189��6�   $��3��6�   5(      5)      �� 5*      -   5        $ Step2GroupBox��5*      $��Step 2��66   $
��Left=�6F   $��
234,10,0,0q�6a   $	��Top=�6u   $	��376��6�   $	��211��6�   .��5+      � $
��Left=�6   $	��366��6:   $	��Top=�6G   $	��211��6_   $��0,0,-12,-12q�6k   .+�5,      �� -   5        
 ��Name$
 ConnectButton��5,   %   $��Connect��6<   $
��Left=�6N   $��	10,81,0,0q�6i   $	��Top=�6|   $	��179��6�   $ -0.549,-0.139&�6�   $��23��6�   6�   �� 5-      -   5        $
 DisconnectButton��5-   %   $��
Disconnect��6?   $
��Left=�6T   $��
10,114,0,0q�6o   $	��Top=�6�   $	��179��6�   $��22��6�   6�   �� 5.      -   5        $	 Step2TextBlock��5.   (   $
��Left=�6@   $��	10,10,0,0q�6[   $
��Wrap=�6n   $9��3Click the Connect button to establish a connection.��6�   $	��Top=�6�   $��71��6�   $	��191��6�   �� 5/      -   5        $
 
TestButton��5/   %   $
��Test��69   $
��Left=�6H   $��
10,295,0,0q�6c   $	��Top=�6w   $	��179��6�   $ 0.157,0.827&�6�   $��48��6�   6�   �� 50      -   5         ��Name$ ConnectionButtonGrey��50   $   $
��Left=�6B   $	��122��6]   $��
40,160,0,0q�6j   $	��Top=�6~   $	��120��6�   $��Images/button_grey.png��6�   �� 51      -   5        $ ConnectionButtonOrange��51   $   $
��Left=�6D   $	��120��6_   $��
40,160,0,0q�6l   $	��Top=�6�   $	��120��6�   $��Images/button_orange.png��6�   52      53      �� 54      -   5        $ DirectTrackLogo��54      $��38��65   $��463,19,22,339q�6A   $��Center=�6X   $	��295��6s   $H��BImages/DILO-DIRECT TRACK RGB Logo TRANSPARENT_trimmed_rescaled.png��6    � DataContext 55         WSProj.CommandType 56      ReadLiteralValue=�6B   58      �� 59      -   5        $	 DirectTrackDescription��59       $
��Left=�6@   $��
463,62,0,0q�6[   $
��Wrap=�6o   $x��rDirect-Track is the only cloud-based SF6 gas management software designed to track inventory and report emissions.��6�   $	��Top=�6�   $��69��6  $	��295��6!  �� 5:      -   5        $	 DiloHomePageLink��5:       $
��Left=�6:   $��463,131,0,0q�6U   $
��Wrap=�6j   $	��Top=�6~   $	��167��6�   �� 5;      -   5        $��http://www.dilo.com/?�5;   #   .�6i   Dilo Home Page5=      5>      �� 5?      -   5        $	 DiloDirectHomePageLink��5?       $
��Left=�6@   $��463,147,0,0q�6[   $
��Wrap=�6p   $	��Top=�6�   $	��167��6�   �� 5@      -   5        $ ��http://www.dilodirect.com/?�5@       .�6r   Dilo Direct Home Page5B      5C      �� 5D      -   5        $	 DiloAddress��5D       $
��Left=�65   $��463,212,0,0q�6P   $
��Wrap=�6e   $(��"11642 Pyramid Dr. Odessa FL, 33556��6y   $	��Top=�6�   $��17��6�   $	��295��6�   �� 5E      -   5        $	 DiloAddress_Copy��5E       $
��Left=�6:   $��463,229,0,0q�6U   $
��Wrap=�6j   $ ��Main Phone: (727) 612-8797��6~   $	��Top=�6�   $��17��6�   $	��295��6�   �� 5F      -   5        $	 DirectTrackLink��5F       $
��Left=�69   $��463,163,0,0q�6T   $
��Wrap=�6i   $	��Top=�6}   $	��167��6�   �� 5G      -    5        $%��http://www.dilodirecttrack.com/?�5G       .�6x   Direct-Track Home Page5I      5J      5K      5L      �� 5M      $��Weigh��6   .��5N      � ���	#FFE5E5E5 5O      �� 5P      $��8* �6+   �� 5Q      $
��217* �6+   �� 5R      $	��35* �6+   .+�5T      �� -!   5         ��Name$ WeightTextBox��5T      $
��Left=�65   $��54��6P   $��	10,13,0,0q�6\    �� TextWrapping$
 Wrap=�6o   $��0.0 LB��6�   $	��Top=�6�   $	��760��6�   $��48(�6�   $��	Digital-7)�6�   $��3��6�   �� 5U      -"   5        $ SerialNumberTextBox��5U      $
��Left=�6;   $��23��6V   $��
12,133,0,0q�6b   $
 Wrap=�6v   $	��Top=�6�   $	��107��6�   $��2��6�   �� 5V      -#   5        $ SerialNumberLabel��5V      $��Serial Number:��67   $
��Left=�6P   $��
12,107,0,0q�6k   $	��Top=�6   $	��107��6�   $��2��6�   �� 5W      -$   5        $ RecordPreviewTextBox��5W      $
��Left=�6<   $��23��6W   $��
12,161,0,0q�6c   $
 Wrap=�6w   $	��Top=�6�   $	��758��6�   {�.  $��3��6�   �� 5X      -%   5        $
 StartingWeightButton��5X      $��Set Start Weight��6;   $
��Left=�6V   $��109,133,0,0q�6q   $	��Top=�6�   $	��100��6�   $��1��6�   �� 5Y      -&   5        $
 EndingWeightButton��5Y      $��Set End Weight��69   $
��Left=�6R   $��224,133,0,0q�6m   $	��Top=�6�   $��95��6�   $��1��6�   �� 5Z      -'   5        $
 RemoveRecordButton��5Z      $��Remove��69   $
��Left=�6J   $��
66,193,0,0q�6e   $	��Top=�6y   $��75��6�   $��1��6�   �� 5[      -(   5        $
 AddRecordButton��5[      $	��Add��66   $
��Left=�6D   $��
10,193,0,0q�6_   $	��Top=�6s   $��75��6�   $��2��6�   &   System.Windows.Controls.DataGrid  5\      -)   5          Name$ WeightRecordDataGrid��5\      $
��Left=�6=   $��
10,222,0,0q�6X   $	��Top=�6l   $	��167��6�   $	��760��6�      AutoGenerateColumns .  $��3��6�      Columns 5]      0  *System.Windows.Controls.DataGridTextColumn  5^      ,  &System.Windows.Controls.DataGridColumn   Header$ Serial Number��6u      CanUserResize .     Width5  /System.Windows.Controls.DataGridLengthConverter$	 120 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *SerialNumber+   ClipboardContentBinding 6R   Z� 6�     5_      $ Start Weight��6w    .  $	 150 6�    61   �� *StartingWeight+ 6T   Z� 6�     5`      $ 
End Weight��6u    .  $	 150 6�    61   �� *EndingWeight+ 6R   Z� 6�   5b      �� 5c      -*   5        $ AutoWeighCheckbox��5c      $��
Auto Weigh��6:   $
��Left=�6O   $��	10,72,0,0q�6j   $	��Top=�6}   $	��128��6�   $��2��6�   �� 5d      -+   5        $
 ExportButton��5d      $��Export��63   $
��Left=�6D   $��
22,193,0,0q�6_   $	��Top=�6s   $��73��6�   $��2��6�   5e      5f      �� 5g      $��	Debugging��6   .��5h      � ���	#FFE5E5E5.+�5i      �� -,   5        $ DebuggingTextBox��5i      $
��Left=�68   $	��122��6S   $
 Wrap=�6`   $�� ��6t   $	��Top=�6|   $	��760��6�   " �� VerticalScrollBarVisibility$
 Auto=�6�   $��
10,186,0,0q�6�   5j      5k      5l      5m      5n      