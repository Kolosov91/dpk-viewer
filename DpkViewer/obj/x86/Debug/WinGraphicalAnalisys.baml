   M S B A M L   `   `   `  ����D  @DpkViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:DpkViewer	DpkViewer  5   	   ;"clr-namespace:DpkViewer.ConvertersDpkViewer.Converters  5   	   L HListStringViewWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullP:clr-namespace:ListStringViewWPF;assembly=ListStringViewWPFListStringViewWPF 5   �   R NConstructGraphicLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullbFclr-namespace:ConstructGraphicLibrary;assembly=ConstructGraphicLibraryConstructGraphicLibrary 6  $    DpkViewer.WinGraphicalAnalisys   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   'locclr-namespace:DpkViewer     5   	   9
converters"clr-namespace:DpkViewer.Converters     5   	   Jmy1:clr-namespace:ListStringViewWPF;assembly=ListStringViewWPF     5   �   UmyFclr-namespace:ConstructGraphicLibrary;assembly=ConstructGraphicLibrary     6    � Title$:  4Побитный графический анализ��6	   $	��477��6-   $	��888��6:    � Topmost .   � WindowStartupLocation$ CenterOwner=�6V    � Icon$N H/DpkViewer;component/Resources/MainMenu/Operations/GraphicalAnalisys.ico��6z    � 	Resources 5      ,   &DpkViewer.Converters.TimeMarkConverter   TimeMarkConverter,   &DpkViewer.Converters.TimeSpanConverter  TimeSpanConverter  TimeMarkDataTemplate �� 	Converter  buttonStyle �� 
TargetType �� Property �� Value  buttonSimpleStyle��%o  &	        &	      &	 &     7   &	 a     &	 �       5   
   6'     5	   
   6'   �� 5
   
   .R�5      �� ��6   �� 8   5      �� 5   
   " ��6$   .�5      ��  � ��$ 35��6'   �� 5       � ��$ 35��6&   5      �� 5   
   " ��6*   .�5      ��  � ��$ 35��6'   �� 5       � ��$ 35��6&   5      5      	 J� CommandBindings	 5      )  #System.Windows.Input.CommandBinding  5   
   -   5        
   Command$7
 1loc:GraphicalAnalisysCommands.ScaleUpTimeInterval��5      6U     5   
   -   5        $9
 3loc:GraphicalAnalisysCommands.ScaleDownTimeInterval��5      6W     5   
   -   5        $+
 %loc:GraphicalAnalisysCommands.AddMark��5      6I     5   
   -   5        $4
 .loc:GraphicalAnalisysCommands.ChooseActiveMark��5      6R     5   
   -   5        $2
 ,loc:GraphicalAnalisysCommands.ShowActiveMark��5      6j     5   
   -   5        $4
 .loc:GraphicalAnalisysCommands.RemoveActiveMark��5      6R     5   
   -   5        $4
 .loc:GraphicalAnalisysCommands.ShowMarkInterval��5      6n     5   
   -   5        $.
 (loc:GraphicalAnalisysCommands.AddGraphic��5      6L     5   
   -	   5        $1
 +loc:GraphicalAnalisysCommands.RemoveGraphic��5      6h     5    
   -
   5        $4
 .loc:GraphicalAnalisysCommands.SetActiveGraphic��5       6S     5!   
   -   5        $1
 +loc:GraphicalAnalisysCommands.ResetInterval��5!      6O   .��5#      � .+�5$   
   5  /ConstructGraphicLibrary.HorizontalTimeScrollBar  -   5        $��horizontalTimeScrollBar��5$   9   $��
12,0,12,12q�6%   $��Bottom=�6X   $��31��6�   �� 5%   
   $)��#Временной интервал��6   $
��Left=�6/   $	��126��6J   $��
601,12,0,0q�6V   $	��143��6j   $	��Top=�6w   .��5&      � .+�5'      �� $9��3loc:GraphicalAnalisysCommands.ScaleDownTimeInterval��6    � ToolTip$) #Уменьшить интервал��6]   #��[ 6z   $��	15,71,0,0q�6�   $
��Left=�6�   $��83��6�   $��35��6�   $	��Top=�6�   .��5(      �� $4��./DpkViewer;component/Resources/Other/Minus.ico��6   5)      �� 5*      -   5        $��textBlockInterval��5*      $��
15,50,16,0q�5+      $��Courier New)�61   $
��Bold"�6J    �� TextAlignment$ Center=�6\   $��16��6s   $	��Top=�6   ��5*   5   ��  �� ElementName$ horizontalTimeScrollBar�� �� Path$ VisibilityTimeInterval�# [ 5+      �� 5,      $7��1loc:GraphicalAnalisysCommands.ScaleUpTimeInterval��6   $) #Увеличить интервал��6[   #��[ 6x   $��15,5,0,0q�6�   $��35��6�   $	��Top=�6�   $
��Left=�6�   $��83��6�   .��5-      �� $3��-/DpkViewer;component/Resources/Other/Plus.ico��6   5.      5/      50      �� 52   
   $Z��TИзмерение временного интервала между метками��6   $
��Left=�6J   $��
293,12,0,0q�6e   $	��292��6z   $	��143��6�   $	��Top=�6�   .��53      � .+�54      �� $��16��6   $��6,9,0,0q�6(   $$��Начальная метка:��69   $	��Top=�6Q   $
��Left=�6i   $��96��6�   �� 55      $��16��6   $��6,86,0,0q�6(   $"��Конечная метка:��6:   $	��Top=�6Q   $
��Left=�6i   $��96��6�   �� 56      -   5        $��comboBoxMarkEnd��56   9   $��
108,82,0,0q�6%   $��10(�57   %   $��27��63   $	��Top=�6?   $
��Left=�58   %   $	��161��6@   #��[ 59   %    �� 
IsReadOnly . 6l   �� 5:      -   5        $��comboBoxMarkBegin��5:   .   $��	108,5,0,0q�6   $��10(�6G   $��27��5;      $	��Top=�6'   $
��Left=�6?   $	��161��5<      #��[ 6'   6\   �� 5=      $��
108,49,0,0q�6   $��Интервал:��60   $
��Left=�6A   $��56��6\   $��21��6g   $	��Top=�6s   �� 5>      -   5        $��textBlockIntervalValue��5>   $   $��
169,51,0,0q�6B   $��00:00:00:000��6V   $��Courier New)�6j   $
��Bold"�6�   $
��Left=�6�   $��82��6�   $��22��6�   $	��Top=�6�   �� 5?      $4��.loc:GraphicalAnalisysCommands.ShowMarkInterval��6   #��[ 6R   $��6,39,0,0q�6}   $
��Left=�6�   $��35��6�   $��35��6�   $	��Top=�6�   .��5@      �� $:��4/DpkViewer;component/Resources/Other/SetInterval.ico��6$   5A      5C      5D      �� 5E      $*��$Операции над меткой��6   $��67��68   $
��Left=�6D   $��	12,12,0,0q�6_   $	��Top=�6r   $	��265��6�   .��5F      � .+�5G      �� -   5        $��comboBoxMarks��5G   .   $��	46,14,0,0q�6   $��10(�6C   $��27��6Q   $	��Top=�5H      $
��Left=�63   $	��161��6N    �� Text$# -Активная метка-��5I   M    . #��[ 5J      5L      �� 5M      $2��,loc:GraphicalAnalisysCommands.ShowActiveMark��6   #��[ 6T   $��0,9,6,0q�6   $��Right=�6�   $��35��6�   $��35��6�   $	��Top=�6�   .��5N      �� $3��-/DpkViewer;component/Resources/Other/Mark.ico��6$   5O      �� 5P      $4��.loc:GraphicalAnalisysCommands.RemoveActiveMark��6   #��[ 6R   $��6,9,0,0q�6}   $��35��6�   $	��Top=�6�   $
��Left=�6�   $��35��6�   .��5Q      �� $9��3/DpkViewer;component/Resources/Other/RemoveMark.ico��6   5R      5S      5T      2  ,ConstructGraphicLibrary.AutoScaleListGraphic  5U   
   -   5        $��multiLayerGraphic_Canvas��5U   8   $��12,161,12,58q�6"   6�   �� 5W   
   $0��*Операции над графиками��6   $��76��63   $
��Left=�6?   $��	12,79,0,0q�6Z   $	��Top=�6m   $	��265��6�   .��5X      � .+�5Y      �� $.��(loc:GraphicalAnalisysCommands.AddGraphic��6   #��[ 6L   $��6,7,0,0q�6w   $
��Left=�6�   $��35��6�   $��35��6�   $	��Top=�6�   .��5Z      �� $7��1/DpkViewer;component/Resources/Other/Interval.ico��6   5[      �� 5\      $1��+loc:GraphicalAnalisysCommands.RemoveGraphic��6   #��[ 6O   $��0,7,6,0q�6z   $��Right=�6�   $��35��6�   $��35��6�   $	��Top=�6�   .��5]      �� $5��//DpkViewer;component/Resources/Other/Cancel.ico��6   5^      �� 5_      -   5        $��comboBoxGraphics��5_   U   $��29��6   $
��Left=�6'   $��	46,10,0,0q�6B   $	��Top=�6m   $	��161��6�    . $��10(�6�   6�   5`      5a      �� 5b   
   $#��Сброс интервала��6   $��67��6,   $
��Left=�68   $��
744,12,0,0q�6S   $	��Top=�6g   $	��110��6   .��5c      � .+�5d      �� $1��+loc:GraphicalAnalisysCommands.ResetInterval��6   #��[ 6O   $��32,7,0,0q�6z   $
��Left=�6�   $��35��6�   $��35��6�   $	��Top=�6�   .��5e      �� $8��2/DpkViewer;component/Resources/Other/Attention.ico��6   5f      5g      5h      �� 5i   
   $ ��Аппроксимация��6   $��76��6*   $
��Left=�66   $��
744,79,0,0q�6Q   $	��Top=�6e   $	��110��6}   .��5j      � .+�5k      �� -   5        $��imageApproximation��5k   Q   $��30��6   $
��Left=�6$   $��34,7,0,0q�6?   $
��Fill=�6k   $	��Top=�6z   $��30��6�   $>��8/DpkViewer;component/Resources/Other/NoApproximation.ico��6�   5l      5m      5n      5o      