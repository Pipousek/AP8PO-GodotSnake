# Code Review
#### Petr Nagy - Mikuláš Mikeska
#### Code from - J. Varhaník, P. Mikulecký
<hr />

## Assignment
```
1) Functionality and Correctness:
    - Verify that the existing functionality is preserved and that the code behaves as expected.
    - Check for any logical errors, boundary conditions, and edge cases.
2) Code Clarity and Readability:
    - Assess the code’s readability. Is it easy to understand?
    - Look for descriptive variable and function names.
    - Evaluate the use of comments and documentation.
3) Code Structure and Modularity:
    - Examine the overall structure of the code. Is it modular?
    - Identify any overly complex or monolithic functions.
    - Check if functions break down into smaller, reusable components.
4) Coding Standards and Conventions:
    - Check if the code adheres to established coding standards.
    - Check consistency in formatting and style.
5) Coding Standards and Conventions:
    - Was UI well separated from app logic?
    - Was it understandable?
    - Was it easy to replace with Godot GUI?
```

## Our Review
```
1)  - Only one simple mistake in our given code was regererating snake body after eating berry.
    - Overall experience from given code is almost excelent.

2)  - Code was pretty easy to read and understeand beside of lack of comments.
    - Few variables weren't needed, because they make the code a bit wanky, but the rest of them were OK.
    - Function names decribe exactly their work.

3)  - Code is modular enough to be easily understand by another code reviewer.
    - Code don't contain any complex structures or functions.

4)  - UI was separated from application logic.
    - The whole code was formatted in same style.

5)  - UI was well separated from application logic.
    - Code was overall well understandable.
    - Overall it was pretty easy to replace it with Godot gui, but the Godot itself is pretty weird to work with.
```

## Final Grade --- 2 points