Imports System
Imports System.Formats.Asn1

Module Program
    Dim ring(,) As Integer = {{0, 32, 15, 19, 4, 21, 2, 25, 17, 34, 6, 27, 13, 36, 11, 30, 8, 23, 10, 5, 24, 16, 33, 1, 20, 14, 31, 9, 22,18, 29, 7, 28, 12, 35, 3, 26},
                              {0, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2}}
    Dim colors() As ConsoleColor = {ConsoleColor.White, ConsoleColor.Red, ConsoleColor.Black}
    Dim ringRank0() As Integer = {0, 32, 15, 19, 4, 21, 2, 25, 17, 34, 6, 27, 13, 36, 11, 30, 8, 23, 10, 5, 24, 16, 33, 1, 20, 14, 31, 9, 22,18, 29, 7, 28, 12, 35, 3, 26}
    Function GetIndex(mass() As Integer, obj As Integer) As Integer
        For i = 0 To UBound(mass)
            If mass(i) = obj Then Return i
        Next
    End Function
    
    Function spinWheel() As String()
        Dim rnd As New Random
        Dim probability(36) As Double
        Dim whatDropped As New List(Of String)
        
        For k = 0 To rnd.Next(1,5)
            For i = 0 To rnd.Next(1,rnd.Next(5,100))
                rnd.NextDouble()
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
        
        whatDropped.Add(ring(0,dropped).ToString())
        
        If ring(0, dropped) mod 2 = 0 And dropped > 0
            whatDropped.Add("EVEN")
        Else
            whatDropped.Add("ODD")
        End If
        
        If ring(0, dropped) mod 3 = 0 And dropped > 0
            whatDropped.Add("3L")
        Else If ring(0, dropped) mod 3 = 1
            whatDropped.Add("2L")
        Else
            whatDropped.Add("1L")
        End If
        
        If ring(1,dropped) = 1
            whatDropped.Add("RED")
        Else If  ring(1,dropped) = 2
            whatDropped.Add("BLACK")
        End If
        
        If ring(0, dropped) > 0 And ring(0, dropped) <= 12
            whatDropped.Add("F12")
        Else If ring(0, dropped) > 12 And ring(0, dropped) <= 24   
            whatDropped.Add("S12")
        Else If ring(0, dropped) > 24
            whatDropped.Add("T12")
        End If
        
        If ring(0,dropped) > 0 And ring(0,dropped) < 19
            whatDropped.Add("TO18")
        ElseIf ring(0,dropped) > 18
            whatDropped.Add("FROM18")
        End If
        
        Return whatDropped.ToArray()
    End Function
    
    Sub display(dropped As Integer)
        Console.BackgroundColor = ConsoleColor.Green
        Console.Clear()
        For i = 0 To 36
            If i <> dropped
                Console.ForegroundColor = colors(ring(1,i))
                Console.Write(ring(0,i) & " ")
            Else
                Console.BackgroundColor = ConsoleColor.White
                Console.ForegroundColor = colors(ring(1,i))
                Console.Write(ring(0, i))
                Console.BackgroundColor = ConsoleColor.Green
                Console.Write(" ")
            End If
        Next
        Dim temp As Integer
        Console.WriteLine()
        Console.WriteLine()
        For i = 3 To 36 Step 3
            temp = GetIndex(ringRank0, i)
            If i <> ring(0, dropped)
                Console.ForegroundColor = colors(ring(1,temp))
                Console.Write(i & " ")
            Else
                Console.BackgroundColor = ConsoleColor.White
                Console.ForegroundColor = colors(ring(1,temp))
                Console.Write(i)
                Console.BackgroundColor = ConsoleColor.Green
                Console.Write(" ")
            End If
        Next
        Console.WriteLine()
        For i = 2 To 36 Step 3
            temp = GetIndex(ringRank0, i)
            If i <> ring(0, dropped)
                Console.ForegroundColor = colors(ring(1,temp))
                Console.Write(i & " ")
            Else
                Console.BackgroundColor = ConsoleColor.White
                Console.ForegroundColor = colors(ring(1,temp))
                Console.Write(i)
                Console.BackgroundColor = ConsoleColor.Green
                Console.Write(" ")
            End If
        Next
        Console.WriteLine()
        For i = 1 To 36 Step 3
            temp = GetIndex(ringRank0, i)
            If i <> ring(0, dropped)
                Console.ForegroundColor = colors(ring(1,temp))
                Console.Write(i & " ")
            Else
                Console.BackgroundColor = ConsoleColor.White
                Console.ForegroundColor = colors(ring(1,temp))
                Console.Write(i)
                Console.BackgroundColor = ConsoleColor.Green
                Console.Write(" ")
            End If
        Next
    End Sub
    
    Sub rules()
        Console.Clear
        Console.WriteLine("Правила:")
        Console.WriteLine("Игра представляет собой Европейскую рулетку. Игрок должен сделать ставку на определённую зону, будь то число, сектор, строка, чётность или цвет. Игрок может делать несколько ставок. Ставки вводятся в предоставленную зону через пробел.")
        Console.WriteLine("<число 0 - 36> - ставка на число (1:36).")
        Console.WriteLine("1L/2L/3L - 1/2/3 линия соответственно, снизу вверх (1:3).")
        Console.WriteLine("F12/S12/T12 - ставка на сектора от 1 по 12/от 13 по 24/от 25 по 36 соответственно (1:3).")
        Console.WriteLine("RED/BLACK - ставка на цвет (1:2).")
        Console.WriteLine("TO18/FROM18 - ставка на сектор от 1 по 18/от 19 по 36 (1:2).")
        Console.WriteLine("EVEN/ODD - чётные/нечётные (1:2).")
        Console.WriteLine("Игрок изначально получает 5000 фишек.")
        Console.WriteLine("После того, как игрок укажет, на что ставит, он указывает ОДНО количество фишек на каждую ставку.")
        Console.WriteLine("Удачи!")
    End Sub
    
    Sub game()
        
    End Sub
    
    Sub Main()
        Console.BackgroundColor = ConsoleColor.Green
        Randomize()
        Console.Clear()
        Console.ForegroundColor = ConsoleColor.Black
        'Console.SetWindowSize(108,100)
        Console.WriteLine("Игра Рулетка")
        Console.WriteLine("1) Начать игру")
        Console.WriteLine("2) Ознакомиться с правилами")
        Console.WriteLine("3) Выйти из игры")
        Dim n As Integer = Console.ReadLine
        Select Case n
            Case 1
                game()
                Main()
            Case 2
                rules()
                Main()
            Case 3
                Exit Sub
        End Select
        Console.ReadKey()
    End Sub
End Module
