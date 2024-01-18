Imports System.ComponentModel
Imports System.Text

Namespace MyMath

    ''' <summary>
    ''' Provides a simple light bit vector with easy integer or booleanean access to a 64 bit storage.
    ''' </summary>
    Public Structure BitVector64
        Implements ICloneable

#Region "MASKS"

        Private Shared ReadOnly _MASK As ULong() = {
            &H1,
            &H2,
            &H4,
            &H8,
            &H10,
            &H20,
            &H40,
            &H80,
            &H100,
            &H200,
            &H400,
            &H800,
            &H1000,
            &H2000,
            &H4000,
            &H8000,
            &H10000,
            &H20000,
            &H40000,
            &H80000,
            &H100000,
            &H200000,
            &H400000,
            &H800000,
            &H1000000,
            &H2000000,
            &H4000000,
            &H8000000,
            &H10000000,
            &H20000000,
            &H40000000,
            &H80000000UL,
            &H100000000,
            &H200000000,
            &H400000000,
            &H800000000,
            &H1000000000,
            &H2000000000,
            &H4000000000,
            &H8000000000,
            &H10000000000,
            &H20000000000,
            &H40000000000,
            &H80000000000,
            &H100000000000,
            &H200000000000,
            &H400000000000,
            &H800000000000,
            &H1000000000000,
            &H2000000000000,
            &H4000000000000,
            &H8000000000000,
            &H10000000000000,
            &H20000000000000,
            &H40000000000000,
            &H80000000000000,
            &H100000000000000,
            &H200000000000000,
            &H400000000000000,
            &H800000000000000,
            &H1000000000000000,
            &H2000000000000000,
            &H4000000000000000,
            &H8000000000000000UL
        }

        Private Shared ReadOnly _REVERSE As Byte() = {
            0,
            128,
            64,
            192,
            32,
            160,
            96,
            224,
            16,
            144,
            80,
            208,
            48,
            176,
            112,
            240,
            8,
            136,
            72,
            200,
            40,
            168,
            104,
            232,
            24,
            152,
            88,
            216,
            56,
            184,
            120,
            248,
            4,
            132,
            68,
            196,
            36,
            164,
            100,
            228,
            20,
            148,
            84,
            212,
            52,
            180,
            116,
            244,
            12,
            140,
            76,
            204,
            44,
            172,
            108,
            236,
            28,
            156,
            92,
            220,
            60,
            188,
            124,
            252,
            2,
            130,
            66,
            194,
            34,
            162,
            98,
            226,
            18,
            146,
            82,
            210,
            50,
            178,
            114,
            242,
            10,
            138,
            74,
            202,
            42,
            170,
            106,
            234,
            26,
            154,
            90,
            218,
            58,
            186,
            122,
            250,
            6,
            134,
            70,
            198,
            38,
            166,
            102,
            230,
            22,
            150,
            86,
            214,
            54,
            182,
            118,
            246,
            14,
            142,
            78,
            206,
            46,
            174,
            110,
            238,
            30,
            158,
            94,
            222,
            62,
            190,
            126,
            254,
            1,
            129,
            65,
            193,
            33,
            161,
            97,
            225,
            17,
            145,
            81,
            209,
            49,
            177,
            113,
            241,
            9,
            137,
            73,
            201,
            41,
            169,
            105,
            233,
            25,
            153,
            89,
            217,
            57,
            185,
            121,
            249,
            5,
            133,
            69,
            197,
            37,
            165,
            101,
            229,
            21,
            149,
            85,
            213,
            53,
            181,
            117,
            245,
            13,
            141,
            77,
            205,
            45,
            173,
            109,
            237,
            29,
            157,
            93,
            221,
            61,
            189,
            125,
            253,
            3,
            131,
            67,
            195,
            35,
            163,
            99,
            227,
            19,
            147,
            83,
            211,
            51,
            179,
            115,
            243,
            11,
            139,
            75,
            203,
            43,
            171,
            107,
            235,
            27,
            155,
            91,
            219,
            59,
            187,
            123,
            251,
            7,
            135,
            71,
            199,
            39,
            167,
            103,
            231,
            23,
            151,
            87,
            215,
            55,
            183,
            119,
            247,
            15,
            143,
            79,
            207,
            47,
            175,
            111,
            239,
            31,
            159,
            95,
            223,
            63,
            191,
            127,
            255
            }

#End Region '/MASKS

#Region "CTORs"

        ''' <summary>
        ''' Initializes a new instance of the BitVector64 structure with the specified internal data.
        ''' </summary>
        Public Sub New(Optional data As UInt64 = 0)
            _Data = CULng(data)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the BitVector64 structure with the information in the specified value.
        ''' </summary>
        Public Sub New(value As BitVector64)
            _Data = value.Data
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the BitVector64 structure with the information in the specified value.
        ''' </summary>
        ''' <remarks>
        ''' Not particulary efficient.
        ''' </remarks>
        Public Sub New(value As Boolean())
            If (value Is Nothing) Then
                Throw New ArgumentNullException("value")
            End If

            If (value.Length <= 0) OrElse (value.Length > 64) Then
                Throw New IndexOutOfRangeException("The array provided sould be bound to the lenght between (1,64).")
            End If

            _Data = 0
            For i As Integer = 0 To value.Length - 1
                If value(i) Then
                    Me.Set(i)
                End If
            Next
        End Sub

#End Region '/CTORs

#Region "PROPS"

        ''' <summary>
        ''' Returns the raw data stored in me bit vector.
        ''' </summary>
        Public ReadOnly Property Data As ULong
            Get
                Return _Data
            End Get
        End Property
        Private _Data As ULong

#End Region '/PROPS

#Region "METHODS"

        ''' <summary>
        ''' Return the current status of a flag in the BitVector.
        ''' </summary>
        ''' <param name="index">Index of the bit to get.</param>
        ''' <returns>State of selected bit.</returns>
        Public Function [Get](index As Integer) As Boolean
            If (index < 0 Or index > 63) Then
                Throw New IndexOutOfRangeException($"Index should be bount to (0-63) values, actual Index :{index}")
            End If
            Return (_Data And _MASK(index)) <> 0
        End Function

        ''' <summary>
        ''' Mark as on a bit in the vector.
        ''' </summary>
        ''' <param name="index">Index of the bit to mark (left to right from 0 to 63).</param>
        Public Function [Set](index As Integer) As BitVector64
            If (index < 0 Or index > 63) Then
                Throw New IndexOutOfRangeException($"Index should be bount to (0-63) values, actual Index :{index}")
            End If
            _Data = _Data Or _MASK(index)
            Return Me
        End Function

        ''' <summary>
        ''' Mark as off a bit in the vector.
        ''' </summary>
        ''' <param name="index">Index of the bit to mark (left to right from 0 to 63).</param>
        Public Function Unset(index As Integer) As BitVector64
            If (index < 0 Or index > 63) Then
                Throw New IndexOutOfRangeException($"Index should be bount to (0-63) values, actual Index :{index}")
            End If
            _Data = _Data And (Not _MASK(index))
            Return Me
        End Function

        ''' <summary>
        ''' Change the bit status
        ''' </summary>
        ''' <param name="index">Index of bit to change</param>
        ''' <param name="status">The New bit status (true = 1, false = 0)</param>
        Public Function Apply(index As Integer, status As Boolean) As BitVector64
            Return If(status, Me.Set(index), Me.Unset(index))
        End Function

        Public Overrides Function ToString() As String
            Dim sb As New StringBuilder(64)
            Dim locdata As ULong = _Data
            For i As Integer = 0 To 63
                If ((locdata And &H1UL) <> 0) Then
                    sb.Append("1")
                Else
                    sb.Append("0")
                End If
                locdata >>= 1
            Next
            Return sb.ToString()
        End Function

        ''' <summary>
        ''' Debug only, print a formatted bit string.
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Function StringFormatted() As String
            Dim bits As Char() = Me.ToString().ToCharArray()
            Dim temp As List(Of Char) = bits.ToList()
            temp.Reverse()
            bits = temp.ToArray()

            Dim trunk As String() = New String(7) {}
            For i As Integer = 7 To 0 Step -1
                trunk(7 - i) = String.Join(" ", bits.Skip(i * 8).Take(8))
            Next
            Dim result As String = $"Actual Layout :
 ========================================================================
║        7        |        6        |        5        |        4        |        3        |        2        |        1        |        0        ║
║ 7 6 5 4 3 2 1 0 | 7 6 5 4 3 2 1 0 | 7 6 5 4 3 2 1 0 | 7 6 5 4 3 2 1 0 | 7 6 5 4 3 2 1 0 | 7 6 5 4 3 2 1 0 | 7 6 5 4 3 2 1 0 | 7 6 5 4 3 2 1 0 ║
║_________________|_________________|_________________|_________________|_________________|_________________|_________________|_________________║
║                 |                 |                 |                 |                 |                 |                 |                 ║
║ {trunk(7)} | {trunk(6)} | {trunk(5)} | {trunk(4)} | {trunk(3)} | {trunk(2)} | {trunk(1)} | {trunk(0)} ║
 ========================================================================
        "
            Return result
        End Function

        ''' <summary>
        ''' Apply the bits starting form certain bit of the bitvector, till a certain length of the supplied bits.
        ''' </summary>
        ''' <param name="bits">UInt64 that retain the interesting bits</param>
        ''' <param name="bitVectorApplyPoint">Starting point in the current bitvector (0,63).</param>
        ''' <param name="bitsLength">Define how many of the supplied bits have to be taken into account (1,64).If there Is Not enought space after the apply point, bits are discarded.</param>
        Public Function InsertBits(bits As ULong, bitVectorApplyPoint As Integer, bitsLength As Integer) As BitVector64
            If (bitVectorApplyPoint < 0 OrElse bitVectorApplyPoint > 63) Then
                Throw New IndexOutOfRangeException($"BitVectorApplyPoint should be bount to (0,63) values, actual Value :{bitVectorApplyPoint}")
            End If

            If (bitsLength < 1 OrElse bitsLength > 64) Then
                Throw New IndexOutOfRangeException($"BitsLength should be bount to (1,64) values, actual Value :{bitsLength}")
            End If

            Dim temporary As ULong = 0
            Dim firstStageBlankShift As Integer = 0
            Dim secondStageBlankShift As Integer = 64 - bitsLength
            Dim thirdStageBlankShift As Integer = 0

            'first stage of original data, if any
            If (bitVectorApplyPoint > 0) Then
                firstStageBlankShift = 64 - bitVectorApplyPoint
                temporary = (_Data << firstStageBlankShift) >> firstStageBlankShift
            End If

            'second stage of inserted data (mandatory)
            secondStageBlankShift = 64 - bitsLength
            temporary = temporary Or ((bits << secondStageBlankShift) >> secondStageBlankShift) << bitVectorApplyPoint

            'third stage of original data, if any
            thirdStageBlankShift = bitVectorApplyPoint + secondStageBlankShift
            If (thirdStageBlankShift < 64) Then
                temporary = temporary Or (_Data >> thirdStageBlankShift) << thirdStageBlankShift
            End If

            _Data = temporary
            Return Me
        End Function

        ''' <summary>
        ''' Reverse the bits of the current BitVector64
        ''' </summary>
        ''' <returns>Return the current BitVector64</returns>
        Public Function Reverse() As BitVector64
            Dim b0 As ULong = _Data And &HFFUL
            Dim b1 As ULong = _Data And &HFF00UL
            Dim b2 As ULong = _Data And &HFF0000UL
            Dim b3 As ULong = _Data And &HFF000000UL
            Dim b4 As ULong = _Data And &HFF00000000UL
            Dim b5 As ULong = _Data And &HFF0000000000UL
            Dim b6 As ULong = _Data And &HFF000000000000UL
            Dim b7 As ULong = _Data And &HFF00000000000000UL

            b0 = _REVERSE(CInt(b0))
            b1 = _REVERSE(CInt(b1 >> 8))
            b2 = _REVERSE(CInt(b2 >> 16))
            b3 = _REVERSE(CInt(b3 >> 24))
            b4 = _REVERSE(CInt(b4 >> 32))
            b5 = _REVERSE(CInt(b5 >> 40))
            b6 = _REVERSE(CInt(b6 >> 48))
            b7 = _REVERSE(CInt(b7 >> 56))

            _Data = 0
            _Data = _Data Or (b0 << 56) Or (b1 << 48) Or (b2 << 40) Or (b3 << 32) Or (b4 << 24) Or (b5 << 16) Or (b6 << 8) Or b7
            Return Me
        End Function

        ''' <summary>
        ''' Rotate the current array by a given numer of bits.
        ''' </summary>
        ''' <param name="bits">Numbe of bits that need to be rotate (-256,256).</param>
        Public Function Rotate(bits As Integer) As BitVector64
            If (bits < -256 OrElse bits > 256) Then
                Throw New IndexOutOfRangeException($"Bits should be bount to (-256,256) values, actual value: {bits}")
            End If
            _Data = (_Data << bits) Or (_Data >> (64 - bits))
            Return Me
        End Function

        ''' <summary>
        ''' Shift the current array by a given numer of bits.
        ''' </summary>
        ''' <param name="bits">Numbe of bits that need to be rotate (-64,64)</param>
        Public Function Shift(bits As Integer) As BitVector64
            If (bits < -63) OrElse (bits > 63) Then
                Throw New IndexOutOfRangeException($"Bits should be bount to (-63,63) values, actual value: {bits}")
            End If
            _Data = If(bits > 0, _Data << bits, _Data >> -bits)
            Return Me
        End Function

        ''' <summary>
        ''' Join the data of two vector.
        ''' </summary>
        ''' <param name="vector">Vector to join with current vector.</param>
        ''' <returns>The current vector update.</returns>
        Public Function Union(vector As BitVector64) As BitVector64
            _Data = _Data Or vector.Data
            Return Me
        End Function

        ''' <summary>
        ''' Intersect the data of two vector.
        ''' </summary>
        ''' <param name="vector">Vector to intersect with current vector.</param>
        ''' <returns>The current vector update.</returns>
        Public Function Intersect(vector As BitVector64) As BitVector64
            _Data = _Data And vector.Data
            Return Me
        End Function

        ''' <summary>
        ''' Flip all bits in the current BitVector64
        ''' </summary>
        ''' <returns>Return the updated BitVector64</returns>
        Public Function Negate() As BitVector64
            _Data = Not _Data
            Return Me
        End Function

        ''' <summary>
        ''' Join the data of two vector, excluding the intersection.
        ''' </summary>
        ''' <param name="vector">Vector to exclusevelly unite with current vector.</param>
        Public Function ExclusiveUnion(vector As BitVector64) As BitVector64
            _Data = _Data Xor vector.Data
            Return Me
        End Function

        ''' <summary>
        ''' Get the bit that are in current BitVector64 but not in the given one.
        ''' </summary>
        ''' <param name="vector">Provided BitVector64.</param>
        Public Function Left(vector As BitVector64) As BitVector64
            _Data = _Data Or vector.Data
            _Data = _Data And (Not vector.Data)
            Return Me
        End Function

        ''' <summary>
        ''' Get the bit that are in the given BitVector64 but not in the current one.
        ''' </summary>
        ''' <param name="vector">Provided BitVector64.</param>
        Public Function Right(vector As BitVector64) As BitVector64
            Dim original As ULong = _Data

            _Data = _Data Or vector.Data
            _Data = _Data And (Not original)

            Return Me
        End Function

        ''' <summary>
        ''' Return a new BitVector64 built arround the given subset.
        ''' </summary>
        ''' <param name="subsetStart">Start of selection (0,63).</param>
        ''' <param name="subsetEnd">End of selection (0,63).</param>
        Public Function SubsetAtoB(subsetStart As UShort, subsetEnd As UShort) As BitVector64
            If (subsetStart < 0 OrElse subsetStart > 63 OrElse subsetEnd < 0 OrElse subsetEnd > 63) Then
                Throw New IndexOutOfRangeException($"Boundaries must be in the range (0,63) , actual : ({subsetStart},({subsetEnd})")
            End If

            Dim internalState As ULong = _Data
            internalState <<= 63 - subsetEnd
            internalState >>= subsetStart + (63 - subsetEnd)
            Return New BitVector64(internalState)
        End Function

        ''' <summary>
        ''' Return a new BitVector64 built arround the given subset.
        ''' </summary>
        ''' <param name="subsetStart">Start of selection (0,63).</param>
        ''' <param name="subsetLength">Number of bits to select (could be negative if you would take bits before start).</param>
        Public Function SubsetATillLength(subsetStart As UShort, Optional subsetLength As Short = 0) As BitVector64
            If (subsetStart < 0 OrElse subsetStart > 63) Then
                Throw New IndexOutOfRangeException($"SubsetStart must be in the range (0,63) , actual : {subsetStart}")
            End If

            If (subsetLength = 0) Then
                subsetLength = CShort(63 - subsetStart)
            End If

            Dim firstBoundary As Integer = subsetStart
            Dim secondBoundary As Integer = subsetStart + subsetLength + If(subsetLength > 0, -1, 1)

            If (firstBoundary > secondBoundary) Then
                Dim tempBoundary As Integer = secondBoundary
                secondBoundary = firstBoundary
                firstBoundary = tempBoundary
            End If

            If (firstBoundary < 0 OrElse firstBoundary > 63 OrElse secondBoundary < 0 OrElse secondBoundary > 63) Then
                Throw New IndexOutOfRangeException($"Boundaries must be in the range (0,63) , actual : ({firstBoundary},({secondBoundary})")
            End If

            Return Me.SubsetAtoB(CUShort(firstBoundary), CUShort(secondBoundary))
        End Function

        ''' <summary>
        ''' Generate a deepcopy of current element.
        ''' </summary>
        Public Function Clone() As Object Implements ICloneable.Clone
            Return New BitVector64(_Data)
        End Function

        Public Overrides Function Equals(o As Object) As Boolean
            If (Not (o Is GetType(BitVector64))) Then
                Return False
            End If
            Return _Data = CType(o, BitVector64).Data
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return _Data.GetHashCode()
        End Function

#End Region '/METHODS

    End Structure

End Namespace