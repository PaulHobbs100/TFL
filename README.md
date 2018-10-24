TfL Shop Excercise

Progress notes/assumptions:
 - Removed existing 2 unit tests
 - Replaced with 10 unit tests of my own, 2 may be out of scope ?
 - Marked code changes or suggesions to follow up on with TODO markers in code
 - refactored balance calculaion as was clearly wrong, tagged with TODO comment
 - try/catch not added yet, want to see all raw errors before wrapping in error handler
 - 'unique' Stock items not handled in code 
 - stockitems added to tests (scope?), woudld need to MOQ or stub to test for valid stock item numbers 
 - Assumed 0 quantity is allowed, just remove from basket prior to order
 - Assumed 0 unitPrice is OK , could be a promotion ?
 - Assumed negative quantity is allowed, could be a 'return' of goods ?
 - Assumed negative unitcost is allowed, could be a 'refund'
 - Single responsibility principle in checkoutservice, also does email, being pragmatic, just one line
 - No customer distinction in customer account, could be issue if need to maintain state for multiple orders
 - considering extra unit test, where basket contains many entries for same stock item must have same unitprice ?
 - Considering using Log4net to log cases where basket order is only partially satisfied and require further investiagtion
 
 Thats quite a lot of discussion points to get the ball rolling, hope it was what you were expecting
 
