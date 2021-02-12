Module Module1

    Sub Main()
        While True
            Try

                Console.WriteLine("Scrivi qui il testo da sommare: ")
                Dim testo = Console.ReadLine()

                Dim numero = Calcola(testo)
                Console.WriteLine($"Totale: {numero}")

            Catch ex As Exception
                Console.WriteLine($"Si è verificato un errore: {ex.Message}{vbNewLine}{ex.GetBaseException.Message}")
                'Console.WriteLine("Si è verificato un errore. Verificare di aver rispettato il formato indicato.")
            End Try
        End While
    End Sub

    Private Function Calcola(testo As String) As Integer
        Dim somma As Integer
        Dim numero As Integer

        If String.IsNullOrEmpty(testo) Then Return 0

        Dim parole = testo.Split(",")

        ' Escludo le parole che non sono numeri validi e sommo il resto
        For Each parola In parole
            If Integer.TryParse(parola, numero) Then
                somma += numero
            End If
        Next

        Return somma
    End Function

End Module
