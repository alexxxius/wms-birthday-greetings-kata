# The birthday greetings kata

### Purpose
To learn about the hexagonal architecture, which is a good way to structure an application, and how to shield your domain model from external apis and systems.

### Problem: write a program that
Loads a set of employee records from a flat file.
Sends a greetings email to all employees whose birthday is today.
The flat file is a sequence of records, separated by newlines (CSV).

This are the first few lines:
    
    last_name, first_name, date_of_birth, email
    Doe, John, 1982/10/08, john.doe@foobar.com
    Ann, Mary, 1975/09/11, mary.ann@foobar.com
    
The greetings email contains the following text:
    
    Subject: Happy birthday!
    Happy birthday, dear John!
    
with the first name of the employee substituted for “John”

### Test List

## misc
- parsing con metodi ad-hoc
- parse file content
- estrare un Employee per ovviare all'oggetto anonimo

## no birthday
- file con header but no rows => no sends

## many birthdays

## non happy path
- file mancante => no sends
- file no-head yes rows => no sends
- file full empty => no sends
- righe malformattate
- righe duplicate
- receiver email not valid



# Test Matrix Examples
Video: https://www.youtube.com/watch?v=URSWYvyc42M

class Order
    AddLine(line)
        lines.Add(line)
    SetDiscount(value)
        lookUp(L1).setDiscount(value) 
    Total
        Sum(Lines, acc, cur => acc + cur.LineTotal)

class Line
    Price
    Qty
    LineTotal


class LineTest
    // In - Qry
    var o = Line(10, 1)
    Assert(10, o.Price)

    // In - Qry
    var o = Line(10, 2)
    Assert(20, o.LineTotal)

    // In - Cmd
    var o = Line(10, 1)
    o.SetDiscount(5)
    Assert(5, o.LineTotal)
    
 class OrderTests
    // Order.Total - In - Qry
    // Line.LineTotal - Out - Qry
    var o = Order(Line(10, 1), Line(12, 1))
    Assert(22, o.Total)

    // In - Cmd
    var o = Order(Line(10, 2), Line(12, 1))
    o.addLine(Line(32))
    Assert(62, o.Total)

    // Out - Cmd - Bad version
    var o = Order(mockL1, Line(12, 1))
    o.setDiscount(L1, 5) // lookUp(L1).setDiscount(5)
    Assert(5, o.lookUp(L1).Price)  <-- NO, bad design test
    
    // Out - Cmd - Direct result
    var o = Order(Line(10, 2), Line(12, 1))
    o.setDiscount(L1, 5) // lookUp(L1).setDiscount(5)
    Assert(27, o.Total)
    
    // Out - Cmd - Mock
    var o = Order(mockL1, Line(12, 1))
    o.setDiscount(L1, 5)
    AssertWasCalled(mockL1.setDiscount(5))



### Object stereotypes
Entity/Aggregate/StatefulObject => Order
    - Have identity Order.Number
    - Have lifecycle Placed, InProgress, Done
    - Have mutable state over time
    
ValueObjects => Money, Eur, M3, EurOverM3, Date, DateOfBirth
    - MODEL FIXED QUANTITIES
    - IMMUTABLE
    - NO IDENTITY FIELD
    - EQUAL IF THEY HAVE SAME STATE


Date
DateRage(from, to)
ReservationPeriod(DateRange)
Reservation
    Id: ReservationId
    Period: ReservationPeriod
    
data ReservationId = ReservationId UUID
type ReservationId = ReservationId of GUID
class ReservationId : NewType<ReservationId, Guid> {
        public ReservationId(Guid value) : base(value) {}
}

list<double>
    .Select(x => x.toInt.toString)