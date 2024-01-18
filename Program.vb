Imports MyMath

Public Module Program

    Public Function Main(args As String()) As Integer

        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine("Test 0 - Constructor & Representation")
        Console.ForegroundColor = ConsoleColor.Gray

        Dim vector1 As New BitVector64()
        Console.WriteLine(vector1)
        Dim vector2 As New BitVector64(&H3000000000000003)
        Console.WriteLine(vector2)

        Dim vector3 As New BitVector64(vector2.Negate())
        Console.WriteLine(vector3)

        Dim vector4 As New BitVector64({True, True, True})
        Console.WriteLine(vector4)

        Dim vector5 As New BitVector64(&HB9000C80008C00ABUL)
        Console.WriteLine($"Printing 0x{vector5.Data:X16}{vbNewLine}".Insert(0, vbNewLine))
        Console.WriteLine(vector5)
        Console.WriteLine(vector5.StringFormatted())
        Console.ReadKey()

        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine("Test 1 - Set/Unset".Insert(0, vbNewLine))
        Console.ForegroundColor = ConsoleColor.Gray

        Dim vector As New BitVector64()
        Console.WriteLine("Set:")
        For i As Integer = 0 To 63
            vector.Set(i)
            Console.WriteLine(vector)
        Next
        Console.WriteLine("Unset:")
        For i As Integer = 0 To 63
            vector.Unset(63 - i)
            Console.WriteLine(vector)
        Next
        Console.WriteLine("Apply true")
        For i As Integer = 0 To 63
            vector.Apply(i, True)
            Console.WriteLine(vector)
        Next
        Console.WriteLine("Apply false")
        For i As Integer = 0 To 63
            vector.Apply(63 - i, False)
            Console.WriteLine(vector)
        Next
        Console.ReadKey()

        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine("Test 2 - Read".Insert(0, vbNewLine))
        Console.ForegroundColor = ConsoleColor.Gray

        vector = New BitVector64(&HD110010012B0404BUL)
        Console.WriteLine(vector)

        Dim queryResult As New Text.StringBuilder()
        For i As Integer = 0 To 63
            queryResult.Append(If(vector.Get(i), "T", "F"))
        Next
        Console.WriteLine(queryResult)
        Console.ReadKey()

        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine("Test 3 - Rotation".Insert(0, vbNewLine))
        Console.ForegroundColor = ConsoleColor.Gray

        For i As Integer = -256 To 256
            vector = New BitVector64(13)
            Console.WriteLine(vector.Rotate(i))
        Next
        Console.ReadKey()

        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine("Test 4 - Shift".Insert(0, vbNewLine))
        Console.ForegroundColor = ConsoleColor.Gray

        For i As Integer = 0 To 63
            vector = New BitVector64(&HB00000000000000DUL)
            Console.WriteLine($"{i}){vbTab} {vector.Shift(i)}")
        Next

        Console.WriteLine(vbNewLine)
        For i As Integer = 0 To -63 Step -1
            vector = New BitVector64(&HB00000000000000DUL)
            Console.WriteLine($"{i}){vbTab} {vector.Shift(i)}")
        Next
        Console.ReadKey()

        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine("Test 5 - Intersect & Union".Insert(0, vbNewLine))
        Console.ForegroundColor = ConsoleColor.Gray

        vector1 = New BitVector64(&HB000000000000000UL)
        vector2 = New BitVector64(&H800000000000000BUL)
        Console.WriteLine(vector1)
        Console.WriteLine(vector2)
        Console.WriteLine($"Union     {vector1.Union(vector2)}")
        vector1 = New BitVector64(&HB000000000000000UL)
        Console.WriteLine($"Intersect {vector1.Intersect(vector2)}")
        Console.ReadKey()

        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine("Test 6 - Negate".Insert(0, vbNewLine))
        Console.ForegroundColor = ConsoleColor.Gray
        vector = New BitVector64(&HB000000000000000UL)
        Console.WriteLine(vector)
        Console.WriteLine(vector.Negate())
        Console.WriteLine(vector.Negate())
        Console.ReadKey()

        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine("Test 7 - ExclusiveUnion".Insert(0, vbNewLine))
        Console.ForegroundColor = ConsoleColor.Gray
        vector1 = New BitVector64(&HB000000000000000UL)
        vector2 = New BitVector64(&H800000000000000BUL)
        Console.WriteLine(vector1)
        Console.WriteLine(vector2)
        Console.WriteLine(vector1.ExclusiveUnion(vector2))
        Console.ReadKey()

        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine("Test 8 - Left&Right".Insert(0, vbNewLine))
        Console.ForegroundColor = ConsoleColor.Gray

        vector1 = New BitVector64(&HB0000003C0000000UL)
        vector2 = New BitVector64(&H3C000000DUL)
        Console.WriteLine("Left")
        Console.WriteLine(vector1)
        Console.WriteLine(vector2)
        Console.WriteLine(vector1.Left(vector2))

        vector1 = New BitVector64(&HB0000003C0000000UL)
        vector2 = New BitVector64(&H3C000000DUL)
        Console.WriteLine("Right")
        Console.WriteLine(vector1)
        Console.WriteLine(vector2)
        Console.WriteLine(vector1.Right(vector2))
        Console.ReadKey()

        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine("Test 9 - Reverse".Insert(0, vbNewLine))
        Console.ForegroundColor = ConsoleColor.Gray

        vector1 = New BitVector64(&HA0442251EA451603UL)

        Console.WriteLine($"0x{vector1.Data:X}")
        Console.WriteLine(vector1)
        Console.WriteLine("")
        vector1.Reverse()

        Console.WriteLine($"0x{vector1.Data:X}")
        Console.WriteLine(vector1)
        Console.WriteLine("")
        vector1.Reverse()

        Console.WriteLine($"0x{vector1.Data:X}")
        Console.WriteLine(vector1)
        Console.ReadKey()

        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine("Test 10 - Subset limit & length".Insert(0, vbNewLine))
        Console.ForegroundColor = ConsoleColor.Gray

        vector1 = New BitVector64(&HA0442251EA451603UL)

        Console.WriteLine($"0x{vector1.Data:X}")
        Console.WriteLine(vector1)
        Console.WriteLine(vector1.StringFormatted())

        Console.WriteLine("Limit")
        Console.WriteLine($"0-3{vbNewLine} {vector1.SubsetAtoB(0, 3)}")
        Console.WriteLine($"4-7{vbNewLine} {vector1.SubsetAtoB(4, 7)}")
        Console.WriteLine($"8-11{vbNewLine} {vector1.SubsetAtoB(8, 11)}")
        Console.WriteLine($"12-15{vbNewLine} {vector1.SubsetAtoB(12, 15)}")
        Console.WriteLine($"16-19{vbNewLine} {vector1.SubsetAtoB(16, 19)}")
        Console.WriteLine($"20-23{vbNewLine} {vector1.SubsetAtoB(20, 23)}")
        Console.WriteLine($"24-27{vbNewLine} {vector1.SubsetAtoB(24, 27)}")
        Console.WriteLine($"28-31{vbNewLine} {vector1.SubsetAtoB(28, 31)}")
        Console.WriteLine($"32-35{vbNewLine} {vector1.SubsetAtoB(32, 35)}")
        Console.WriteLine($"36-39{vbNewLine} {vector1.SubsetAtoB(36, 39)}")
        Console.WriteLine($"40-43{vbNewLine} {vector1.SubsetAtoB(40, 43)}")
        Console.WriteLine($"44-47{vbNewLine} {vector1.SubsetAtoB(44, 47)}")
        Console.WriteLine($"48-51{vbNewLine} {vector1.SubsetAtoB(48, 51)}")
        Console.WriteLine($"52-55{vbNewLine} {vector1.SubsetAtoB(52, 55)}")
        Console.WriteLine($"56-59{vbNewLine} {vector1.SubsetAtoB(56, 59)}")
        Console.WriteLine($"60-63{vbNewLine} {vector1.SubsetAtoB(60, 63)}")

        Console.WriteLine("Length")
        Console.WriteLine(vector1)
        Console.WriteLine($"1..{vbNewLine} {vector1.SubsetATillLength(1)}")
        Console.WriteLine($"2..{vbNewLine} {vector1.SubsetATillLength(2)}")
        Console.WriteLine($"32,L9{vbNewLine} {vector1.SubsetATillLength(32, 9)}")
        Console.WriteLine($"32,L-9{vbNewLine} {vector1.SubsetATillLength(32, -9)}")
        Console.WriteLine("")
        Console.ReadKey()

        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine("Test 11 - Insert Bits")
        Console.ForegroundColor = ConsoleColor.Gray

        vector1 = New BitVector64(0)
        Dim bits As ULong = 5

        Console.WriteLine(vector1)
        Console.WriteLine("Inserting '101'" & vbNewLine)

        Dim entryPoint As Integer = 0
        Dim len As Integer = 3
        Do While (entryPoint <= 63)
            Console.WriteLine($"{entryPoint},{len}){vbTab} {vector1.InsertBits(bits, entryPoint, len)}")
            entryPoint += len
        Loop

        Console.WriteLine()
        vector1 = New BitVector64(0)
        bits = 9

        Console.WriteLine(vector1)
        Console.WriteLine("Inserting '1001'" & vbNewLine)

        entryPoint = 0
        len = 4
        Do While (entryPoint <= 63)
            Console.WriteLine($"{entryPoint},{len}){vbTab} {vector1.InsertBits(bits, entryPoint, len)}")
            entryPoint += len
        Loop

        Console.ForegroundColor = ConsoleColor.Yellow
        Console.WriteLine("Press any key to exit...")
        Console.ReadKey()

        Return 0

    End Function

End Module
