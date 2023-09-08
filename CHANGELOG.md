## Release 1.2.0

### Enhancements

**Improved the inclined header labels feature** ([PR #36](https://github.com/CaptainArbitrary/CompactWorkTab/pull/36))

- Limited header drawing to the main Work tab only
  - This is specifically for improved compatibility with Numbers but it should help compatibility generally to make sure we're not running our code in unexpected contexts
- Refactored mod settings to include three possible header styles
  - inclined (the default)
  - vertical
  - horizontal (vanilla behavior)
- Improved the positioning of the sorting icon above its column when inclined labels are enabled
- Redesigned the mod options dialog to be easier to use and more intuitive (also prettier)

**Moved inclined headers to fit over their columns** ([PR #39](https://github.com/CaptainArbitrary/CompactWorkTab/pull/39))

- Calculate the horizontal offset using rect.xMax instead of rect.x + rect.width / 2
- Apply the calculated offset to the rotatedRect for proper positioning

**Enabled mouse-over features for inclined and vertical headers** ([PR #40](https://github.com/CaptainArbitrary/CompactWorkTab/pull/40))

- Inclined and vertical headers now highlight consistently when moused over
- Mouse-over sounds are enabled for inclined and vertical headers
- Inclined and vertical headers now display their tooltips correctly when hovered over

## Release 1.1.1

### Enhancements

**Minor improvements to inclined labels** ([PR #31](https://github.com/CaptainArbitrary/CompactWorkTab/pull/31))

- Significantly refactored the code and clarified the key algorithms
- Repositioned inclined labels to make them less derpy looking
- Added lines under the labels to make the table easier to read

## Release 1.1.0

### New Features

**Added an experimental option to lay out labels at a 60° angle** ([PR #29](https://github.com/CaptainArbitrary/CompactWorkTab/pull/29))

- The option is off by default
- With the option off, the mod behaves in exactly the same way it did before
- With it on, column labels are inclined to a 60° angle
- Also, header highlighting is disabled if inclined headers are turned on

### Enhancements

**Add GapTiny × 2 padding to Cache.MinHeaderHeight** ([PR #28](https://github.com/CaptainArbitrary/CompactWorkTab/pull/28))

## Release 1.0.1

### New Features

**Added mod options** ([PR #16](https://github.com/CaptainArbitrary/CompactWorkTab/pull/16))

### Enhancements

**Used Krafs Publicizer to eliminate some Invoke() calls and improve performance** ([PR #15](https://github.com/CaptainArbitrary/CompactWorkTab/pull/15))

**Patched CalculateHeaderHeight instead of GetMinHeaderHeight** ([PR #17](https://github.com/CaptainArbitrary/CompactWorkTab/pull/17))

PawnTable.CalculateHeaderHeight is called once while PawnColumnWorker_WorkPriority is called N times where N is the number of work priority columns in the table. We only need one.

**Added compatibility with Grouped Pawns Lists** ([PR #19](https://github.com/CaptainArbitrary/CompactWorkTab/pull/19))

See https://steamcommunity.com/sharedfiles/filedetails/?id=2340773428

### Bug Fixes

**Fixed erroneous += operator in Cache.cs** ([PR #13](https://github.com/CaptainArbitrary/CompactWorkTab/pull/13))

## Release 1.0.0

_No changes._

