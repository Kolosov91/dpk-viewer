   M S B A M L   `   `   `  ����D  @DpkViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:DpkViewer	DpkViewer  5   	   ;"clr-namespace:DpkViewer.ConvertersDpkViewer.Converters  5   	       DpkViewer.WinAddGraphic   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   'locclr-namespace:DpkViewer     5   	   9
converters"clr-namespace:DpkViewer.Converters     5   	     � Title$)  #Добавление графика��5   	   $	��182��6$   $	��236��61    � 
ResizeMode$ NoResize=�6=    � ShowInTaskbar .   � Topmost .  � WindowStartupLocation$ CenterOwner=�6x    � 	Resources 5      1   +DpkViewer.Converters.AddressStringConverter   AddressStringConverter  AddressDataTemplate �� 	Converter0   *DpkViewer.Converters.NumBitStringConverter  NumBitStringConverter  NumBitDataTemplate��%�   &	        &	      7   &	 N     &	 a     7    5   
   6,   �� 5	   
   .R�5
      �� ��6   �� 8   5        5   
   6+   �� 5   
   .R�5      �� ��6   �� 8   5      5       J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$. (loc:WinSetValueCommands.AcceptParametres��5      6h     5   
   -   5        $. (loc:WinSetValueCommands.CancelParametres��5      6L   .��5      � .+�5   
   �� $��Адрес:��6   $��28��6!   $
��Left=�6-   $��	42,12,0,0q�6H   $	��Top=�6[   $��43��6s   �� 5   
   $��Номер бита:��6   $��28��6&   $��	12,46,0,0q�62   $	��Top=�6E   $
��Left=�6]   $��73��6x   �� 5   
   -   5        $��comboBox_Addresses��5   3   $��28��6   $��
91,12,12,0q�6   $	��Top=�5      #��[ 6+   �� 5   
   -   5        $��comboBox_NumBits��5   3   $��28��6   $��
91,46,12,0q�6   $	��Top=�5      #��[ 6+   �� 5   
   $.��(loc:WinSetValueCommands.CancelParametres��6   $��0,0,12,8q�6D   $��35��6V   $��Bottom=�6b   $��Right=�6}   .��5      �� 	 �� Orientation$	 
Horizontal=�6   .+�5      �� $5��//DpkViewer;component/Resources/Other/Cancel.ico��6   �� 5      $��Закрыть��6   $��Center=�6*   $��Center=�6G   5       5!      �� 5"   
   $.��(loc:WinSetValueCommands.AcceptParametres��6   $��12,0,0,8q�6D   $��35��6V   $��Bottom=�6b   $
��Left=�6}   .��5#      �� $	 
Horizontal=�6   .+�5$      �� $7��1/DpkViewer;component/Resources/Other/SetValue.ico��6   �� 5%      $��Добавить��6   $��Center=�6+   $��Center=�6H   5&      5'      5(      5)      