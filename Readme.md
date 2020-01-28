
# Monte Carlo Visualization in .NET WPF
The application let's you visualize π approximation. Written using **Windows Presentation Foundation**. 

## Usage
If you just want to run the application you can download the `dist/` folder. Then double click on `MonteCarloVisualization.application`

![MonteCarloVisualization](https://raw.githubusercontent.com/lucky125111/MonteCarloVisualization/master/MonteCarloVisualization.gif)
### Requirements
Application runs only under Windows. If you don't have .NET run time installed run `dist/setup.exe`




## Monte Carlo π Approximation

## Implementation

The [implementation](http://en.wikipedia.org/wiki/Monte_Carlo_method#Introduction "Introduction — Monte Carlo method — Wikipedia") takes a random point inside the square and checks if that point lies within the circle. The ratio of points inside the circle to all generated points is _p_. Multiplying _p_ by 4 gives the approximate value of π.

### Fixed number of points
Calculates π value for given number of points.
### Iterative approximation
Calculates π value in multiple steps, each time for different number of points. You can see as the animation progresses the value of π is approximated with better accuracy.

## Remarks
### 1. WPF is not the best technology to draw a lot of points on Canvas. The default mechanisms of the framework don't support such oppertations.
First of all UI is single thread so each paint operation freezes the application (keep in mind freeze the application is something different than disabling UI controls).
The solution works this way:
* Shapes (rectangle and circle) are drawn on canvas 
* All points are drawn to `WritableBitmap` which is later drawn on `Image`
This way the application performance is much better
### 2. UI clickable elements are disable when animation is in progress
### 3. Most of the controls use data binding
### 4. XAML was created by Visual Studio designer