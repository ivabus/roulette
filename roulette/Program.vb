Imports System.Text

Module Program
	'https://github.com/ivabus/roulette
	'https://ivabus.dev/roulette
	Dim ReadOnly _
		Ring(,) As Integer =
			{{0, 32, 15, 19, 4, 21, 2, 25, 17, 34, 6, 27, 13, 36, 11, 30, 8, 23, 10, 5, 24, 16, 33, 1, 20, 14, 31, 9, 22, 18, 29,
			  7, 28, 12, 35, 3, 26},
			 {0, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2}}
	'Второе измерение массива Ring необходимо для распределения цвета с помощью массива Colors по номерам 0 - белый, 1 - красный, 2 - чёрный

	Dim ReadOnly Colors() As ConsoleColor = {ConsoleColor.White, ConsoleColor.Red, ConsoleColor.Black}

	Dim ReadOnly _
		RingRank0() As Integer = {0, 32, 15, 19, 4, 21, 2, 25, 17, 34, 6, 27, 13, 36, 11, 30, 8, 23, 10, 5, 24, 16, 33, 1, 20, 14, 31, 9, 22, 18, 29,
			  7, 28, 12, 35, 3, 26}
	'ringRank0 нужен, чтобы было удобно подавать массив в поиск индекса

	Private Const ReleaseTag As String = "2.1.7"
	
	Dim _lang As Integer = -1 ' 0 - русский language, 1 - english язык
	
	Dim ReadOnly LocalizationStrings(,)= {{"История выпадений (последние 15): ", "   Нажмите любую кнопку, чтобы начать игру! ",
	                           "Выберите сложность: ", "1) Лёгкая - 500 фишек в начале", "2) Нормальная - 100 фишек в начале",
	                           "3) Сложная - 10 фишек в начале", "4) Невозможная - 2 фишки в начале",
	                           "Выбор некорректен!", "Игра началась!", "Делайте ставки:", "Укажите суммы ставок:", "Ставки не корректны. Пропуск.",
	                           "Крутим колесо...", "Выпало: ", "У Вас {0} фишек.", "Продолжить игру? (Y/n) >>> ", "Неверный ввод, продолжаем игру.",
	                           "У Вас закончились фишки, игра окончена.", "Нажмите любую клавишу, чтобы выйти в меню.",
	                           "Рулетка / roulette", "Автор: Иван Бущик <ivan@bushchik.ru>", "Лицензия: GNU GPLv3", "Сайт: ivabus.dev/roulette",
	                           "Репозиторий: github.com/ivabus/roulette", "Версия: ", "Введите количество чисел для генерирования >>> ",
	                           "Погрешность генератора случайных чисел: ", "Игра Рулетка", "1) Начать игру",
	                           "2) Ознакомиться с правилами", "3) О игре", "Дополнительно:", "4) Проверка генератора случайных чисел",
	                           "0) Выйти из игры", "Ошибка!"},
	                          {"Drop history (last 15): ", "    Press any button to start ", "Choose difficulty: ", "1) Easy - 500 chips on the start",
	                           "2) Normal - 100 chips on the start", "3) Hard - 10 chips on the start",
	                           "4) Impossible - 2 chips on the start", "Incorrect choise!", "Game started",
	                           "Place your bets: ", "Specify the bid amounts: ", "The bids are incorrect. Skipping.",
	                           "Spinning wheel...", "Dropped: ", "You have {0} chips.", "Continue? (Y/n) >>> ",
	                           "Incorrect input, we continue the game.", "You have run out of chips, the game is over.",
	                           "Press any key to exit the menu.", "Roulette", "Author: Ivan Bushchik <ivan@bushchik.ru>",
			"License: GNU GPLv3", "Website: ivabus.dev/roulette", "Repository: github.com/ivabus/roulette",
	                           "Version: ", "Enter the number of numbers to generate >>> ",
	                           "Error of the random number generator: ", "Roulette game", "1) Begin game",
	                           "2) Check the rules", "3) About game", "Additional: ", "4) Checking the random number generator", "0) Exit", "Error!"}}
	Dim ReadOnly Logo() As String = { _
		                                "####    ###   #   #  #      #####  #####  #####  #####",
		                                "#   #  #   #  #   #  #      #        #      #    #     ",
		                                "#   #  #   #  #   #  #      #        #      #    #     ",
		                                "#   #  #   #  #   #  #      #        #      #    #     ",
		                                "####   #   #  #   #  #      ####     #      #    ####  ",
		                                "##     #   #  #   #  #      #        #      #    #     ",
		                                "# #    #   #  #   #  #      #        #      #    #     ",
		                                "#  #   #   #  #   #  #      #        #      #    #     ",
		                                "#   #   ###    ###   #####  #####    #      #    ##### "}

	Private Function GetIndex(mass() As Integer, obj As Integer) As Integer 'Функция получает индекс объекта в массиве
		For i = 0 To UBound(mass)
			If mass(i) = obj Then Return i
		Next
		Return - 1
	End Function

	Sub DisplayHistory(history() As Integer)		'Эта функция не реализована в Game(), потому что это захламляло бы код
		Console.ForegroundColor = ConsoleColor.DarkBlue
		Console.Write(LocalizationStrings(_lang,0))
		For i = If(Ubound(history) > 14, Ubound(history) - 14, 0) To UBound(history)
			Console.ForegroundColor = Colors(Ring(1, getindex(RingRank0, history(i))))
			Console.Write(history(i) & " ")
		Next
		Console.ForegroundColor = ConsoleColor.DarkBlue
	End Sub
	
	Sub Sleep(d As Single)		'Название говорит о предназначении функции
		Dim t As Single = Timer
		Do while Timer - t < d
		Loop
	End Sub

	Sub Intro()
		Console.ForegroundColor = ConsoleColor.DarkBlue
		Console.SetCursorPosition(Console.WindowWidth\2 - Len(Logo(0))\2, Console.WindowHeight()\2 - UBound(Logo) + 3)
		For i = 0 To UBound(Logo)
			Console.WriteLine(Logo(i))
			Console.SetCursorPosition(Console.WindowWidth\2 - Len(Logo(0))\2, Console.GetCursorPosition().Item2)
			Sleep(0.04444)
		Next
		'sleep(5)
		Console.SetCursorPosition(0, 0)
		For i = 0 To (Console.WindowHeight()\2 - UBound(Logo) + 2)\2
			Console.WriteLine(StrDup(Console.WindowWidth - 1, "#"))
			Sleep(0.04444)
		Next
		For i = 0 To Console.WindowHeight - (Console.WindowHeight()\2 - UBound(Logo) + 2) - 3
			Console.Write(StrDup((Console.WindowWidth\2 - Len(Logo(0))\2)\2, "#"))
			Console.SetCursorPosition(Console.WindowWidth - (Console.WindowWidth\2 - Len(Logo(0))\2)\2,
			                          (Console.GetCursorPosition().Item2))
			Console.WriteLine(StrDup((Console.WindowWidth\2 - Len(Logo(0))\2)\2 - 1, "#"))
			Sleep(0.04444)
		Next
		For i = 0 To (Console.WindowHeight()\2 - UBound(Logo) + 2)\2 - 1
			Console.WriteLine(StrDup(Console.WindowWidth - 1, "#"))
			Sleep(0.04444)
		Next
		Dim entr = LocalizationStrings(_lang,1)
		Console.SetCursorPosition((Console.WindowWidth\2) - Len(entr)\2,
		                          Console.WindowHeight - (Console.WindowHeight()\2 - UBound(Logo) + 2)\4 - 3)
		Console.Write(StrDup(Len(entr) + 2, " "))

		Console.SetCursorPosition((Console.WindowWidth\2) - Len(entr)\2,
		                          Console.WindowHeight - (Console.WindowHeight()\2 - UBound(Logo) + 2)\4 - 2)
		Console.Write(entr & "  ")

		Console.SetCursorPosition((Console.WindowWidth\2) - Len(entr)\2,
		                          Console.WindowHeight - (Console.WindowHeight()\2 - UBound(Logo) + 2)\4 - 1)
		Console.Write(StrDup(Len(entr) + 2, " "))
		Console.SetCursorPosition(0, 0)
		Console.ReadKey
		Console.Clear
	End Sub

	Function SpinWheel() As String()
		Dim rnd As New Random
		Dim probability(36) As Double
		Dim whatDropped As New List(Of String)

		For k = 0 To rnd.Next(1, 5)
			For i = 0 To rnd.Next(1, rnd.Next(5, 100))
				rnd.NextDouble()
				Randomize
			Next
		Next

		Dim max As Double = 0
		Dim dropped As Integer

		For i = 0 To 36
			probability(i) = rnd.NextDouble()
			if probability(i) > max then
				max = probability(i)
				dropped = i
			End If
		Next

		whatDropped.Add(Ring(0, dropped).ToString())

		If dropped = 0 Then return whatDropped.ToArray()
		If Ring(0, dropped) mod 2 = 0
			whatDropped.Add("EVEN")
		Else
			whatDropped.Add("ODD")
		End If

		If Ring(0, dropped) mod 3 = 0
			whatDropped.Add("3L")
		Else If Ring(0, dropped) mod 3 = 1
			whatDropped.Add("2L")
		Else
			whatDropped.Add("1L")
		End If

		If Ring(1, dropped) = 1
			whatDropped.Add("RED")
		Else If Ring(1, dropped) = 2
			whatDropped.Add("BLACK")
		End If

		If Ring(0, dropped) > 0 And Ring(0, dropped) <= 12
			whatDropped.Add("F12")
		Else If Ring(0, dropped) > 12 And Ring(0, dropped) <= 24
			whatDropped.Add("S12")
		Else If Ring(0, dropped) > 24
			whatDropped.Add("T12")
		End If

		If Ring(0, dropped) > 0 And Ring(0, dropped) < 19
			whatDropped.Add("TO18")
		ElseIf Ring(0, dropped) > 18
			whatDropped.Add("FROM18")
		End If

		Return whatDropped.ToArray()
	End Function

	Sub Display(dropped As Integer)
		Console.BackgroundColor = ConsoleColor.Green
		Console.Clear()

		If Ring(0, 0) <> dropped
			Console.BackgroundColor = ConsoleColor.Black
			Console.ForegroundColor = Colors(Ring(1, 0))
			Console.Write(Ring(0, 0) & " ")
			Console.BackgroundColor = ConsoleColor.Green
		Else
			Console.BackgroundColor = ConsoleColor.White
			Console.ForegroundColor = ConsoleColor.Black
			Console.Write(Ring(0, 0))
			Console.BackgroundColor = ConsoleColor.Green
			Console.Write(" ")
		End If

		For i = 1 To 36
			If Ring(0, i) <> dropped
				Console.ForegroundColor = Colors(Ring(1, i))
				Console.Write(Ring(0, i) & " ")
			Else
				Console.BackgroundColor = ConsoleColor.White
				Console.ForegroundColor = Colors(Ring(1, i))
				Console.Write(Ring(0, i))
				Console.BackgroundColor = ConsoleColor.Green
				Console.Write(" ")
			End If
		Next

		Dim temp As Integer
		Console.WriteLine()
		Console.WriteLine()

		For i = 3 To 36 Step 3
			temp = GetIndex(RingRank0, i)
			If i <> dropped
				Console.ForegroundColor = Colors(Ring(1, temp))
				Console.Write("{0,2:G} ", i)
			Else
				Console.BackgroundColor = ConsoleColor.White
				Console.ForegroundColor = Colors(Ring(1, temp))
				Console.Write("{0,2:G}", i)
				Console.BackgroundColor = ConsoleColor.Green
				Console.Write(" ")
			End If
		Next

		Console.ForegroundColor = ConsoleColor.DarkBlue
		Console.Write("    3L")
		Console.WriteLine()

		For i = 2 To 36 Step 3
			temp = GetIndex(RingRank0, i)
			If i <> dropped
				Console.ForegroundColor = Colors(Ring(1, temp))
				Console.Write("{0,2:G} ", i)
			Else
				Console.BackgroundColor = ConsoleColor.White
				Console.ForegroundColor = Colors(Ring(1, temp))
				Console.Write("{0,2:G}", i)
				Console.BackgroundColor = ConsoleColor.Green
				Console.Write(" ")
			End If
		Next

		Console.ForegroundColor = ConsoleColor.DarkBlue
		Console.Write("    2L")
		Console.WriteLine()

		For i = 1 To 36 Step 3
			temp = GetIndex(RingRank0, i)
			If i <> dropped
				Console.ForegroundColor = Colors(Ring(1, temp))
				Console.Write("{0,2:G} ", i)
			Else
				Console.BackgroundColor = ConsoleColor.White
				Console.ForegroundColor = Colors(Ring(1, temp))
				Console.Write("{0,2:G}", i)
				Console.BackgroundColor = ConsoleColor.Green
				Console.Write(" ")
			End If
		Next
		
		Console.ForegroundColor = ConsoleColor.DarkBlue
		Console.Write("    1L")
		Console.WriteLine()
		Console.WriteLine("    F12    |    S12    |    T12    ")
		Console.ForegroundColor = ConsoleColor.DarkBlue
	End Sub
	Sub Rules()
		If _lang = 0 Then
			Console.Clear
			Console.ForegroundColor = ConsoleColor.DarkBlue
			Console.WriteLine("Рекомендуемое разрешение консоли: 100x35")
			Console.WriteLine("Минимальное разрешение консоли: 100х20")
			Console.WriteLine("Правила:")
			Console.WriteLine(
				"Игра представляет собой Европейскую рулетку. Игрок должен сделать ставку на определённую зону, будь то число, сектор, строка, чётность или цвет. Игрок может делать несколько ставок. Ставки вводятся в предоставленную зону через пробел.")
			Console.WriteLine("<число 0 - 36> - ставка на число (1:36).")
			Console.WriteLine("1L/2L/3L - 1/2/3 линия соответственно, снизу вверх (1:3).")
			Console.WriteLine("F12/S12/T12 - ставка на сектора от 1 по 12/от 13 по 24/от 25 по 36 соответственно (1:3).")
			Console.WriteLine("RED/BLACK - ставка на цвет (1:2).")
			Console.WriteLine("TO18/FROM18 - ставка на сектор от 1 по 18/от 19 по 36 (1:2).")
			Console.WriteLine("EVEN/ODD - чётные/нечётные (1:2).")
			Console.WriteLine("Игрок изначально получает 500, 100, 10, 2 фишки.")
			Console.WriteLine(
				"После того, как игрок укажет, на что ставит, он указывает количество фишек на каждую ставку через пробел.")
			Console.WriteLine("Например:")
			Console.WriteLine("Делайте ставки >>> 0 16 2L T12 RED ODD")
			Console.WriteLine("Укажите суммы ставок >>> 100 50 500 500 1000 1000")
			Console.WriteLine(
				"ВНИМАНИЕ! Количество ставок должно совпадать с количеством фишек, которые ставите, в примере 6 = 6.")
			Console.WriteLine("Или просто нажмите ENTER, чтобы пропустить ставку.")
			Console.WriteLine("Удачи!")
			Console.ReadKey()
		Else
			Console.Clear
			Console.ForegroundColor = ConsoleColor.DarkBlue
			Console.WriteLine("Recommended console resolution: 100x35")
			Console.WriteLine("Minimum console resolution: 100x20")
			Console.WriteLine("Rules:")
			Console.WriteLine (
				"The game is a European roulette. The player must place a bet on a certain zone, whether it is a number, dozen, column, parity or color. A player can place several bets. Bids are entered into the provided zone separated by a space.")
			Console.WriteLine("<number 0 - 36> - bet on the number (1:36).")
			Console.WriteLine("1L/2L/3L - 1/2/3 line, respectively, from bottom to top (1:3).")
			Console.WriteLine("F12/S12/T12 - bet on sectors from 1 to 12/from 13 to 24/from 25 to 36 respectively (1:3).")
			Console.WriteLine("RED/BLACK - bet on color (1:2).")
			Console.WriteLine("TO18/FROM18 - bet on the dozen from 1 to 18/from 19 to 36 (1:2).")
			Console.WriteLine("EVEN/ODD - even/odd (1:2).")
			Console.WriteLine("The player initially receives 500,100,10 or 2 chips.")
			Console.WriteLine (
				"After the player indicates what he is betting on, he indicates the number of chips for each bet separated by a space.")
			Console.WriteLine("For example:")
			Console.WriteLine("Place bets >>> 0 16 2L T12 RED ODD")
			Console.WriteLine("Specify the bid amounts >>> 100 50 500 500 1000 1000")
			Console.WriteLine(
				"ATTENTION! The number of bets must match the number of chips you bet, in the example 6 = 6.")
			Console.WriteLine("Or just press ENTER to skip the bet.")
			Console.WriteLine("Good luck!")
			Console.ReadKey()
			
		End If
		
	End Sub
	Sub Game()
		Console.WriteLine(LocalizationStrings(_lang, 2))
		Console.WriteLine(LocalizationStrings(_lang, 3))
		Console.WriteLine(LocalizationStrings(_lang, 4))
		Console.WriteLine(LocalizationStrings(_lang, 5))
		Console.WriteLine(LocalizationStrings(_lang, 6))
		Console.Write(">>> ")
		Dim choose As Integer = Console.ReadLine()
		Dim fish As Long
		Select Case choose
			Case 1
				fish = 500
			Case 2
				fish = 100
			Case 3
				fish = 10
			Case 4
				fish = 2
			Case Else
				Console.WriteLine(LocalizationStrings(_lang, 7))
		End Select
		Console.WriteLine(LocalizationStrings(_lang,8))
		Dim history As New List(Of Integer)
		Console.Clear()
		Do while fish > 0
			Console.ForegroundColor = ConsoleColor.DarkBlue
			Dim generated() As String = SpinWheel()
			history.Add(generated(0))

			Console.WriteLine(LocalizationStrings(_lang,9))
			Console.Write(">>> ")
			Dim stav
			stav = UCase(Console.ReadLine()).Split.ToList()
			Console.WriteLine(LocalizationStrings(_lang,10))
			Console.Write(">>> ")
			Dim summ() As String
			summ = Console.ReadLine().Split
			For i = 0 To UBound(summ)
				If not IsNumeric(summ(i)) Then
					Console.WriteLine(LocalizationStrings(_lang,11))
					Continue Do
				End If
			Next
			Dim summs As New List(Of Integer)
			For i = 0 To UBound(summ)
				summs.add(Int(summ(i)))
			Next
			If stav.Count <> summs.Count Or summs.ToArray.Sum() > fish Then
				Console.WriteLine(LocalizationStrings(_lang, 11))
				Continue Do
			End If
			For i = 0 To summs.Count - 1
				If summs(i) < 0 Then
					Console.WriteLine(LocalizationStrings(_lang, 11))
					Continue Do
				End If
			Next
			Console.WriteLine(LocalizationStrings(_lang, 12))
			Sleep(0.5)
			Console.Clear()
			Display(Int(generated(0)))
			Console.ForegroundColor = ConsoleColor.DarkBlue
			Dim indedx As Integer
			For i = 0 To UBound(generated)
				If stav.Contains(generated(i)) Then
					indedx = stav.IndexOf(generated(i))
					If IsNumeric(generated(i))
						fish += summ(indedx)*35
					Else If _
						generated(i) = "RED" Or generated(i) = "BLACK" Or generated(i) = "ODD" Or generated(i) = "EVEN" Or
						generated(i) = "FROM18" Or generated(i) = "TO18" Then
						fish += summ(indedx)*1
					Else If _
						generated(i) = "3L" Or generated(i) = "2L" Or generated(i) = "1L" Or generated(i) = "F12" Or generated(i) = "S12" Or
						generated(i) = "T12" Then
						fish += summ(indedx)*2
					End If
					stav.RemoveAt(indedx)
					summs.RemoveAt(indedx)
				End If
			Next
			For i = 0 To summs.Count - 1
				fish -= summs(i)
			Next
			Console.WriteLine()
			Console.Write(LocalizationStrings(_lang,13))
			For i = 0 To UBound(generated)
				Console.Write(generated(i) & " ")
			Next
			Console.WriteLine()
			DisplayHistory(history.ToArray())
			Console.ForegroundColor = ConsoleColor.DarkBlue
			Console.WriteLine()
			Console.WriteLine(LocalizationStrings(_lang, 14), fish)
			Console.Write(LocalizationStrings(_lang, 15))
			Dim temp As String = Console.ReadLine()
			If temp = "n" or temp = "N" Then
				Exit Sub
			Else IF temp = "" or temp = "y" or temp = "Y"
				Console.Write("")
			Else
				Console.WriteLine(LocalizationStrings(_lang, 16))
			End If
		Loop
		Console.WriteLine(LocalizationStrings(_lang, 17))
		Console.WriteLine(LocalizationStrings(_lang, 18))
		Console.ReadKey()
	End Sub
	
	Sub About()
		Console.Clear()
		Console.WriteLine(LocalizationStrings(_lang, 19))
		Console.WriteLine(LocalizationStrings(_lang, 20))
		Console.WriteLine(LocalizationStrings(_lang, 21))
		Console.WriteLine(LocalizationStrings(_lang, 22))
		Console.WriteLine(LocalizationStrings(_lang, 23))
		Console.WriteLine(LocalizationStrings(_lang, 24) + ReleaseTag)
		Console.WriteLine(LocalizationStrings(_lang, 18))
		Console.ReadKey()
	End Sub

	Sub TestGenerator()
		Randomize()
		Dim count As Long
		Dim rnd As New Random
		Console.Write(LocalizationStrings(_lang, 25))
		Count = Console.ReadLine()
		Dim mass(count) As Double
		For i = 0 to Ubound(mass)
			mass(i) = rnd.NextDouble()
		Next
		Dim pogr As Double
		Dim sr As Double = mass.Sum() / Count
		pogr = Math.abs((sr - 0.5) / 0.5) * 100
		Console.WriteLine(LocalizationStrings(_lang,26) + pogr.ToString("0.#####") + "%")
		Console.ReadKey()
	End Sub
	Sub Menu()
			Console.Clear()
			Console.WriteLine(LocalizationStrings(_lang, 27))
			Console.WriteLine(LocalizationStrings(_lang, 28))
			Console.WriteLine(LocalizationStrings(_lang, 29))
			Console.WriteLine(LocalizationStrings(_lang, 30))
			Console.WriteLine(LocalizationStrings(_lang, 31))
			Console.WriteLine(LocalizationStrings(_lang, 32))
			Console.WriteLine(LocalizationStrings(_lang, 33))
			Console.Write(">>> ")
			Dim input As Integer = Console.ReadLine
			Select Case input
				Case 0
					Exit Sub
				Case 1
					Console.Clear()
					Game()
					Menu()
				Case 2
					Rules()
					Menu()
				Case 3
					About()
					Menu()
				Case 4
					TestGenerator()
					Menu()
				Case Else
					Exit Sub
			End Select
	End Sub

	Sub Main()
		Try
			If OperatingSystem.IsWindows() Then
				Console.SetWindowSize(100,40)
				Console.OutputEncoding = New UTF8Encoding()
			End If
			Console.BackgroundColor = ConsoleColor.Green
			Console.ForegroundColor = ConsoleColor.DarkBlue
			Randomize()
			Console.Clear()
			If _lang = -1 Then
				Console.WriteLine("Choose language / Выберете язык:")
				Console.WriteLine("1) Russian / Русский")
				Console.WriteLine("2) English / Английский")
				Console.Write(">>> ")
				_lang = Console.ReadLine() - 1
				if _lang < 0 or _lang > 1 Then
					Console.WriteLine("Incorrect choose")
					Exit Sub
				End If
				Console.Clear()
			End If
			
			Intro()
			Menu()
		Catch
			Console.WriteLine(LocalizationStrings(_lang, 34))
			Exit Sub
		End Try
	End Sub
End Module
