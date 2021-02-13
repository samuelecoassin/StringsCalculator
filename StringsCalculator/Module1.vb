Imports System.Text.RegularExpressions

Module Module1

    Sub Main()
        While True
            Try

                Console.WriteLine("Scrivi qui il testo da sommare: ")
                Dim testo = Console.ReadLine()

                Dim numero = Calcola(testo)
                Console.WriteLine($"Totale: {numero}")


            Catch ex As Exception
                Console.WriteLine($"Si è verificato un errore: {ex.Message}")
            End Try
        End While
    End Sub

    Private Function Calcola(testo As String) As Integer
        Dim somma As Integer
        Dim numero As Integer
        Dim numeriNegativi As New List(Of Integer)

        If String.IsNullOrEmpty(testo) Then Return 0

        Dim parole = SeparaTesto(testo)
        If parole Is Nothing Then Return 0

        ' Somma solo i numeri validi
        For Each parola In parole
            If Integer.TryParse(parola, numero) Then
                If numero < 0 Then
                    numeriNegativi.Add(numero)
                ElseIf numero < 1000 Then
                    somma += numero
                End If
            End If
        Next

        ' Se sono presenti numeri negativi, lancia un'eccezione
        If numeriNegativi.Any() Then
            Dim elencoNegativi = String.Join(", ", numeriNegativi)
            Throw New Exception($"i numeri negativi non sono consentiti! Numeri indicati: {elencoNegativi}")
        End If

        Return somma
    End Function

    Private Function SeparaTesto(testo As String) As String()
        ' Verifica se sono richiesti delimitatori diversi
        Dim elencoSeparatori As New List(Of String)
        'TODO: implementare ricerca con le espressioni regolari per migliorare le performance e semplificare il codice
        'Dim rgx = New Regex("^\/\/(\[(\w+)\])+\/\/")
        'If rgx.IsMatch(testo) Then
        '    For Each m In rgx.Matches(testo)

        '    Next
        'End If

        If testo.StartsWith("//[") AndAlso testo.Contains("]//") Then
            Dim separatori = testo.Substring(2, testo.IndexOf("]//") - 1)

            For Each sep In separatori.Split("[")
                If Not String.IsNullOrEmpty(sep) Then
                    If Not sep.EndsWith("]") Then Throw New Exception("non è consentito utilizzare ""["" e ""]"" come separatori di numeri.")

                    If sep.Length > 1 Then
                        Dim sep2 = sep.Substring(0, sep.LastIndexOf("]"))
                        If sep2.Contains("]") Then Throw New Exception("non è consentito utilizzare ""["" e ""]"" come separatori di numeri.")

                        elencoSeparatori.Add(sep2)
                    End If
                End If
            Next

            testo = testo.Substring(testo.IndexOf("]//") + 3)
            If String.IsNullOrEmpty(testo) Then Return Nothing
        End If

        If elencoSeparatori.Any() Then
            Return testo.Split(elencoSeparatori.ToArray, StringSplitOptions.RemoveEmptyEntries)
        Else
            Return testo.Split(",")
        End If
    End Function

End Module
