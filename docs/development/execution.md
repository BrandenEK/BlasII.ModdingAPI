# Order of Execution

A mod's core event methods are called at the same time as the game manager's core events, so this chart shows the order in which they are called

---

```mermaid
graph LR
    A[Mod - OnPreInitialize]
    B([Manager - OnInitialize])
    C[Mod - OnInitialize]
    D[Mod - OnRegisterServices]
    F([Manager - OnAllInitialized])
    G[Mod - OnAllInitialized]
    H(GlobalData - Load)
    X(GlobalData - Save)
    Y[Mod - OnDispose]
    Z([Manager - OnDispose])

    subgraph Startup
    A-->B
    B-->C
    C-->D
    D-->F
    F-->G
    G-->H
    end
    subgraph Shutdown
    X-->Y
    Y-->Z
    end
    Startup~~~Shutdown
```