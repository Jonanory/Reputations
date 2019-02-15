# Reputations

## Summary

Reputations is a system that allows characters within a Unity game to have reputations and judge other characters based on those reputations

## Attributes

Each reacter (character forming the opinions) has a set of characteristrics for each reputable(character the opinions are of. These characterstics determines what the reacter thinks that the reputable is like.

When the reputable performs an action that the reacter has to respond to, the attributes of the action is combined with the precieved attributes of the reputable, using the following formula

```
a(n+1) = b * a(n) + (1 - b) * o(n+1)
```

where `a(n)` is the percieved atttributes at point `n` and `o(n)` is the attributes of action `n`. `a` and `o` are each a set of attributes, while the value of  `b` is between 0 and 1.

## Reputations

When the reacter is asked what their general opinion of a reputable, the characteristics the the reacter has percieved in the reputable is compared with what attribute settings the reacter personally prefers.