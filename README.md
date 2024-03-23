Answers to assignment questions:

  JavaScript Validation Experiment

   1. What do you notice is different if anything?
		Commented Out Code: When I tried to create an invalid item, it went to the breakpoint on my OnPost method
		Regular Code: When I tried to create an invalid item, the validation script stopped it from going the OnPost and the client-side validation took precedence.

  3. What do you think including this section does?
		It is client-side validation that will validate the user input on the browser, rather than going to the server-side request handlers to validate the input.

  Async Experiment
	  1. Do you notice any difference?  If so what?
		  I did not notice any difference between the two versions of the code. I believe this has to do with Entity Framework Core's property of storing the database in memory. I believe that if the database were stored on a physical server, then the async version would be faster.
