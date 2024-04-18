# Protar landing procedure<!-- omit from toc -->

1. [Landing point selection](#landing-point-selection)
2. [Landing points calculation](#landing-points-calculation)
3. [Landing procedure start](#landing-procedure-start)
4. [Landing procedure](#landing-procedure)
5. [Landing procedure state machine](#landing-procedure-state-machine)
6. [Landing settings window](#landing-settings-window)

## Landing point selection

```mermaid
flowchart TD
    %% Nodes
    a11[/Set Landing Point/]
    a12{{Wind < 4 m/s>}}
    a13[/Set wind direction/]
    subgraph updateLandingData
        a21[Create Landing Point]
        a22[Create Waiting Point tangent]
        a23[Create Waiting Point]
        a24[Create Fly-over Point]
    end
    a14[Add point markers to map]

    %% Connections
    a11 --> a12
    a12 --> a13
    a13 --> updateLandingData
    a21 --> a22
    a22 --> a23
    a23 --> a24
    updateLandingData --> a14
```

## Landing points calculation

![Landing points](Landing_points_dark.png)

## Landing procedure start

```mermaid
flowchart TD
    %% Nodes
    a11{{Waiting point is >8km away?}}
    a12[Set speed to Holding Speed]
    a13[Send to Waiting Point]
    a14[Set state to GoToWaiting]
    
    %% Connections
    a11 --"no"--> a12
    a12 --> a13
    a13 --> a14
```

## Landing procedure

```mermaid
flowchart LR
    %% Nodes
    a00(( ))

    a11{{UAV connected & armed?}}
    a12[Set state to None]

    a21{{State is GoToWaiting?}}
    a22{{Reached waiting point?}}
    a23[Set state to WaitForSpeed]
    a24[Set speed to Holding Speed]

    a31{{State is WaitForSpeed?}}
    a32{{Reached Holding Speed?}}
    a33[Set state to WaitForTangent]

    a41{{State is WaitForTangent?}}
    a42{{Reached Tangent Point?}}
    a43[Set speed to Approach Speed]
    a44[Send to Landing Point]
    a45[Set state to GoToLand]

    a51{{State is GoToLand?}}
    a52{{Reached Landing Point + Approach Distance?}}
    a53[Set speed to Final Approach Speed]
    a54[Send to Fly-over Point]
    a55[Set state to CloseToLand]

    a61[Calculate chute open position]
    a62{{Reached chute open position?}}
    a63{{Chute is armed?}}
    a64[Open chutes]
    a65[Refresh map markers]

    %% Connections
    a00 --> a11
    a11 --"no"--> a12
    a11 --"yes"--> a00

    a00 --> a21
    a21 --"no"--> a00
    a21 --"yes"--> a22
    a22 --"no"--> a00
    a22 --"yes"--> a23
    a23 --> a24 --> a00

    a00 --> a31
    a31 --"no"--> a00
    a31 --"yes"--> a32
    a32 --"no"--> a00
    a32 --"yes"--> a33 --> a00

    a00 --> a41
    a41 --"no"--> a00
    a41 --"yes"--> a42
    a42 --"no"--> a00
    a42 --"yes"--> a43
    a43 --> a44 --> a45 --> a00

    a00 --> a51
    a51 --"no"--> a00
    a51 --"yes"--> a52
    a52 --"no"--> a00
    a52 --"yes"--> a53
    a53 --> a54 --> a55 --> a00

    a00 --> a61
    a61 --> a62
    a62 --"no"--> a00
    a62 --"yes"--> a63
    a63 --"no"--> a00
    a63 --"yes" --> a64
    a64 --> a65 --> a00
```

The `doLanding` procedure is constantly running in a 5Hz loop.

## Landing procedure state machine

| Name | Meaning |
| --- | --- |
| None | No landing is requested |
| GoToWaiting | UAV is heading to the waiting loiter circle |
| WaitForSpeed | UAV is dropping it's speed to the desired value (Holding Speed) |
| WaitForTangent | UAV is heading to the tangent point on the waiting loiter circle |
| GoToLand | UAV is heading to the landing point |
| CloseToLand | UAV is on final approach to the landing point |
| Land | Not used |

## Landing settings window

![Landing settings window](<Landing settings window.jpg>)
