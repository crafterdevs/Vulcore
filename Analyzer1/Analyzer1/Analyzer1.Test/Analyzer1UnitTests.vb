Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports VerifyVB = Analyzer1.Test.VisualBasicCodeFixVerifier(
    Of Analyzer1.Analyzer1Analyzer,
    Analyzer1.Analyzer1CodeFixProvider)

Namespace Analyzer1.Test
    <TestClass>
    Public Class Analyzer1UnitTest

        'No diagnostics expected to show up
        <TestMethod>
        Public Async Function TestMethod1() As Task
            Dim test = ""
            Await VerifyVB.VerifyAnalyzerAsync(test)
        End Function

        'Diagnostic And CodeFix both triggered And checked for
        <TestMethod>
        Public Async Function TestMethod2() As Task

            Dim test = "
Class {|#0:TypeName|}

    Sub Main()

    End Sub

End Class"

            Dim fixtest = "
Class TYPENAME

    Sub Main()

    End Sub

End Class"

            Dim expected = VerifyVB.Diagnostic("Analyzer1").WithLocation(0).WithArguments("TypeName")
            Await VerifyVB.VerifyCodeFixAsync(test, expected, fixtest)
        End Function
    End Class
End Namespace
